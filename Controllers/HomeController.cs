using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using website_shopping.Models;
using website_shopping.Models.Contexts;

namespace website_shopping.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult DetailProduct(int? id)
    {
        _logger.LogInformation("id : " + id);
        return View(model: id);
    }

    public IActionResult Introduce()
    {
        return View();
    }
    public IActionResult Product()
    {
        return View();
    }
    public IActionResult AgencyPolicy()
    {
        return View();
    }
    public IActionResult Recruitment()
    {
        return View();
    }
    public IActionResult News()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
