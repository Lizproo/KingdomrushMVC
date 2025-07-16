using Microsoft.EntityFrameworkCore;
using KingdomRuMVC.Models;
using System.Collections.Generic;

namespace KingdomRuMVC.Data
{
    public class KingdomDbContext : DbContext
    {
        public KingdomDbContext(DbContextOptions<KingdomDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFactura { get; set; }
    }
}
