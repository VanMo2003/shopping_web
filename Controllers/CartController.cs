using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using website_shopping.Models;
using website_shopping.Models.Contexts;

namespace website_shopping.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ShopContext _shopContext;
        private readonly UserManager<UserModel> _UserManager;

        public CartController(ILogger<CartController> logger, ShopContext shopContext, UserManager<UserModel> UserManager)
        {
            _logger = logger;
            _shopContext = shopContext;
            _UserManager = UserManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public async Task<IActionResult> AddCart(int? id, [FromForm] int? quantity)
        {
            var email = _UserManager.GetUserName(User);
            var user = _shopContext.Users.Where(u => u.Email == email).FirstOrDefault();

            var product = _shopContext.Products.Where(p => p.Id == id).FirstOrDefault();

            if (user.carts != null)
            {
                bool checkExist = false;
                foreach (var c in user.carts.Split(","))
                {
                    if (c.Split("-")[0].Equals(id + ""))
                    {
                        checkExist = true;
                        int quantityOld = int.Parse(c.Split("-")[1]);
                        user.carts = user.carts.Replace(c, id + "-" + (quantity + quantityOld));
                    }
                }
                if (!checkExist)
                {
                    user.carts += product.Id + $"-{quantity},";

                }
            }
            else
            {
                user.carts = product.Id + $"-{quantity},";
            }

            _shopContext.Users.Update(user);
            await _shopContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var email = _UserManager.GetUserName(User);
            var user = _shopContext.Users.Where(u => u.Email == email).FirstOrDefault();

            var arrPro = user.carts.Split(",");

            foreach (var item in arrPro)
            {
                if (item.Split("-")[0].Equals(id + ""))
                {
                    user.carts = user.carts.Replace(item + ",", "");
                }
            }

            _shopContext.Users.Update(user);
            await _shopContext.SaveChangesAsync();
            return RedirectToAction("GetAll", "Cart");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}