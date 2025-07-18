using Microsoft.EntityFrameworkCore;
using KingdomRuMVC.Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Clave primaria compuesta para DetalleFactura
            modelBuilder.Entity<DetalleFactura>()
                .HasKey(df => new { df.Id_Factura, df.Id_Producto });

            base.OnModelCreating(modelBuilder);
        }
    }
}
