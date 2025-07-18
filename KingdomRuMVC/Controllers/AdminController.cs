using Microsoft.AspNetCore.Mvc;

namespace KingdomRuMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() => RedirectToAction("Panel");

        public IActionResult Ventas() => View();

        public IActionResult Panel()
        {
            var rol = HttpContext.Session.GetString("rol");

            if (rol != "Admin")
            {
                return RedirectToAction("Login", "Usuarios");
            }

            return View(); // Esto carga Views/Admin/Panel.cshtml
        }


    }
}
