using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using NETCore.Encrypt;
using website_shopping.Models;
using website_shopping.Models.Contexts;

namespace website_shopping.Controllers
{
    public class UserController : Controller
    {
        private readonly ShopContext _context;
        private readonly IConfiguration _configuration;
        private readonly string? _key;
        private readonly string? _iv;
        public UserModel UserModel { set; get; }
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, ShopContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _key = _configuration.GetValue<string>("AES:Key");
            _iv = _configuration.GetValue<string>("AES:Iv");
        }
        public IActionResult Login()
        {
            Console.WriteLine("Login get");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Username, Password")] UserModel userModel)
        {

            if (userModel.CheckValidLogin())
            {
                UserModel? user = _context.Users.Where(user => user.Username == userModel.Username).FirstOrDefault();
                if (user != null)
                {
                    string passwordDencrypted = EncryptProvider.AESDecrypt(user.Password, _key, _iv);
                    _logger.LogInformation("check: " + user.Password + "-" + passwordDencrypted);

                    if (passwordDencrypted == userModel.Password)
                    {
                        _logger.LogInformation("Login successfully : " + userModel.Username);
                        if (userModel.Username == "admin")
                        {
                            return RedirectToAction("Product", "Admin", (object)userModel.Username);
                        }
                        return RedirectToAction("Index", "Home", (object)userModel.Username);
                    }
                }
                ViewData["Error"] = "Tài khoản hoặc mật khẩu không chính xác";
                return View();
            }

            return View();

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Username, Password, Fullname, Address, PhoneNumber")] UserModel userModel)
        {
            // Regex regex = new Regex(@"0\d{9}");
            // _logger.LogInformation("check phone number : " + regex.IsMatch(userModel.PhoneNumber));

            if (ModelState.IsValid)
            {
                var passwordEncrypted = EncryptProvider.AESEncrypt(userModel.Password, _key, _iv);
                userModel.Password = passwordEncrypted;
                _logger.LogInformation("register:" + userModel.Username + "-" + userModel.Password + "-" + userModel.Fullname + "-" + userModel.Address + "-" + userModel.PhoneNumber);
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}