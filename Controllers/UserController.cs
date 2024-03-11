using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using website_shopping.Models;

namespace website_shopping.Controllers
{
    public class UserController : Controller
    {
        public UserModel UserModel { set; get; }
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        public IActionResult LoginAsync()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("Username", "Password")] UserModel userModel)
        {
            _logger.LogInformation("login:" + userModel.Username + "-" + userModel.Password);
            return RedirectToAction("Product", "Admin");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([Bind] UserModel userModel)
        {
            string Username = userModel.Username;
            if (true)
            {

            }
            _logger.LogWarning("register:" + userModel.Username + "-" + userModel.Password + "-" + userModel.Fullname + "-" + userModel.Email);
            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}