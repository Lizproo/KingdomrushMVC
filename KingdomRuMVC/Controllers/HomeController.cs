using System.Diagnostics;
using KingdomRuMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace KingdomRuMVC.Controllers
{
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
        public IActionResult Accesorios()
        {
            return View();
        }
        public IActionResult Consolas()
        {
            return View();
        }
        public IActionResult Tunicas()
        {
            return View();
        }
        public IActionResult JuePS45()
        {
            return View();
        }
        public IActionResult JueXbox()
        {
            return View();
        }
        public IActionResult JueNintendo()
        {
            return View();
        }
        public IActionResult JuePC()
        {
            return View();
        }
        public IActionResult Login()
        {
            return Redirect("/Usuarios/Login");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
