using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KingdomRuMVC.Data;
using KingdomRuMVC.Models;

namespace KingdomRuMVC.Controllers
{
    public class CarritoController : Controller
    {
        private readonly KingdomDbContext _context;

        public CarritoController(KingdomDbContext context)
        {
            _context = context;
        }

        // MOSTRAR CARRITO (Mochila)
        public async Task<IActionResult> Mochila()
        {
            var carrito = await _context.Carrito
                .Include(c => c.Producto)
                .ToListAsync();

            return View("Mochila", carrito);
        }

        // ELIMINAR ITEM DEL CARRITO
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var item = await _context.Carrito.FindAsync(id);
            if (item != null)
            {
                _context.Carrito.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Mochila");
        }

        // VERIFICAR EXISTENCIA (opcional)
        private bool CarritoExists(int id)
        {
            return _context.Carrito.Any(e => e.Id_Carrito == id);
        }
    }
}
