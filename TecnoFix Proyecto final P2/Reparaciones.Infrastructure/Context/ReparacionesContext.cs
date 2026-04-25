using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Entities;

namespace Reparaciones.Infrastructure.Context
{
    public class ReparacionesContext : DbContext
    {
        public ReparacionesContext(DbContextOptions<ReparacionesContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Electrodomestico> Electrodomesticos { get; set; }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<Reparacion> Reparaciones { get; set; }
        public DbSet<Garantia> Garantias { get; set; }
        public DbSet<ReparacionRepuesto> ReparacionRepuestos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReparacionRepuesto>()
                .HasKey(rr => new { rr.ReparacionId, rr.RepuestoId });

            modelBuilder.Entity<ReparacionRepuesto>()
                .HasOne(rr => rr.Reparacion)
                .WithMany(r => r.ReparacionRepuestos)
                .HasForeignKey(rr => rr.ReparacionId);

            modelBuilder.Entity<ReparacionRepuesto>()
                .HasOne(rr => rr.Repuesto)
                .WithMany(r => r.ReparacionRepuestos)
                .HasForeignKey(rr => rr.RepuestoId);

            modelBuilder.Entity<Garantia>()
                .HasOne(g => g.Reparacion)
                .WithOne(r => r.Garantia)
                .HasForeignKey<Garantia>(g => g.ReparacionId);

            modelBuilder.Entity<Garantia>()
                .Ignore(g => g.EstaVigente);
        }
    }
}
