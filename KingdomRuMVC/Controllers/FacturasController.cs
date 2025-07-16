using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KingdomRuMVC.Data;
using KingdomRuMVC.Models;

namespace KingdomRuMVC.Controllers
{
    public class FacturasController : Controller
    {
        private readonly KingdomDbContext _context;

        public FacturasController(KingdomDbContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facturas.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .FirstOrDefaultAsync(m => m.Id_Factura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Factura,Id_Usuario,Fecha,Subtotal,Iva,Total,Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id_Factura,Id_Usuario,Fecha,Subtotal,Iva,Total,Estado")] Factura factura)
        {
            if (id != factura.Id_Factura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.Id_Factura))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .FirstOrDefaultAsync(m => m.Id_Factura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(string id)
        {
            return _context.Facturas.Any(e => e.Id_Factura == id);
        }
        [HttpPost]
        public async Task<IActionResult> Comprar([FromBody] CompraRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.IdUsuario) || request.Productos == null || !request.Productos.Any())
                return BadRequest();

            // Crear la factura
            string idFactura = "FAC" + Guid.NewGuid().ToString("N").Substring(0, 7).ToUpper();
            var factura = new Factura
            {
                Id_Factura = idFactura,
                Id_Usuario = request.IdUsuario,
                Fecha = DateTime.Now,
                Subtotal = request.Productos.Sum(p => p.Precio_Unitario * p.Cantidad),
                Iva = 15,
                Total = request.Productos.Sum(p => p.Precio_Unitario * p.Cantidad) * 1.15m,
                Estado = "PAG"
            };
            _context.Facturas.Add(factura);

            // Crear los detalles de factura
            foreach (var prod in request.Productos)
            {
                var detalle = new DetalleFactura
                {
                    Id_Factura = idFactura,
                    Id_Producto = prod.Id_Producto,
                    Cantidad = prod.Cantidad,
                    Precio_Unitario = prod.Precio_Unitario,
                    Estado_Detalle = "PAG"
                };
                _context.DetalleFactura.Add(detalle);
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                // Devuelve el mensaje de error para depuración
                return StatusCode(500, ex.ToString());
            }
        }


        // DTO para recibir la compra
        public class CompraRequest
        {
            public string IdUsuario { get; set; }
            public List<ProductoCompra> Productos { get; set; }
        }

        public class ProductoCompra
        {
            public string Id_Producto { get; set; }
            public string Nombre { get; set; }
            public decimal Precio_Unitario { get; set; }
            public string Imagen_Url { get; set; }
            public string Talla { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
