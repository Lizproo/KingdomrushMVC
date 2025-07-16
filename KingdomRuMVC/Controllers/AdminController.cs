using Microsoft.AspNetCore.Mvc;

namespace KingdomRuMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() => RedirectToAction("Panel");

        public IActionResult Panel() => View();

        public IActionResult Ventas() => View();
    }
}
