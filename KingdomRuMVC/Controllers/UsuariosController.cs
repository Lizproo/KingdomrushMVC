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
    public class UsuariosController : Controller
    {
        private readonly KingdomDbContext _context;

        public UsuariosController(KingdomDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Edad,Correo,Telefono,Direccion,Clave")] Usuario usuario)
        {
            usuario.IdUsuario = Guid.NewGuid().ToString("N").Substring(0, 7).ToUpper();
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Usuarios");
        }


        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id_Usuario,Nombre,Apellido,Edad,Correo,Telefono,Direccion,Clave")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: Usuarios/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Login
        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == model.Correo);

            if (usuario == null || usuario.Clave != model.Clave)

            {
                ViewBag.LoginError = "Correo o contraseña incorrectos.";
                return View(model);
            }

            // ✅ Guardar sesión
            HttpContext.Session.SetString("usuarioLogueado", usuario.Nombre);
            HttpContext.Session.SetString("idUsuario", usuario.IdUsuario);
            HttpContext.Session.SetString("rol", usuario.Correo == "admin@admin.com" ? "Admin" : "Cliente");

            // ✅ Redirigir correctamente según rol
            if (usuario.Correo == "admin@admin.com")
            {
                return RedirectToAction("Panel", "Admin");
            }

            // ✅ Usuario normal
            ViewBag.UsuarioLogueado = usuario.Nombre;
            ViewBag.IdUsuario = usuario.IdUsuario;
            return View("LoginSuccess");
        }



        private bool UsuarioExists(string id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Cierra sesión y borra todos los datos
            return RedirectToAction("Login", "Usuarios");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            // Cifrar contraseña
            usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);

            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }



    }
}

