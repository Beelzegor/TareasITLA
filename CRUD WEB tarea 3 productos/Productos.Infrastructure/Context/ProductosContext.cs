using Microsoft.EntityFrameworkCore;
using Productos.Server.Models;

namespace Productos.Infrastructure.Context
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>().HasIndex(c => c.Nombre).IsUnique();
        }
    }
}
