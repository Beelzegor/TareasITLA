using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;
using Reparaciones.Infrastructure.Context;

namespace Reparaciones.Infrastructure.Repositories
{
    public class ReparacionRepository : IReparacionRepository
    {
        private readonly ReparacionesContext _db;

        public ReparacionRepository(ReparacionesContext db) => _db = db;

        public async Task<IEnumerable<Reparacion>> GetAll() =>
            await _db.Reparaciones
                .AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.Tecnico)
                .Include(r => r.Electrodomestico)
                .ToListAsync();

        public async Task<Reparacion?> GetById(int id) =>
            await _db.Reparaciones
                .Include(r => r.Cliente)
                .Include(r => r.Tecnico)
                .Include(r => r.Electrodomestico)
                .Include(r => r.Garantia)
                .Include(r => r.ReparacionRepuestos).ThenInclude(rr => rr.Repuesto)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IEnumerable<Reparacion>> GetByCliente(int clienteId) =>
            await _db.Reparaciones
                .AsNoTracking()
                .Include(r => r.Electrodomestico)
                .Include(r => r.Tecnico)
                .Where(r => r.ClienteId == clienteId)
                .ToListAsync();

        public async Task<IEnumerable<Reparacion>> GetByEstado(string estado) =>
            await _db.Reparaciones
                .AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.Electrodomestico)
                .Where(r => r.Estado == estado)
                .ToListAsync();

        public async Task Add(Reparacion reparacion)
        {
            await _db.Reparaciones.AddAsync(reparacion);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Reparacion reparacion)
        {
            var existing = await _db.Reparaciones.FindAsync(reparacion.Id)
                ?? throw new KeyNotFoundException($"Reparación con ID {reparacion.Id} no encontrada.");
            _db.Entry(existing).CurrentValues.SetValues(reparacion);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Reparacion reparacion)
        {
            _db.Reparaciones.Remove(reparacion);
            await _db.SaveChangesAsync();
        }
    }
}
