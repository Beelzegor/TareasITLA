using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;
using Reparaciones.Infrastructure.Context;

namespace Reparaciones.Infrastructure.Repositories
{
    public class RepuestoRepository : IRepuestoRepository
    {
        private readonly ReparacionesContext _db;

        public RepuestoRepository(ReparacionesContext db) => _db = db;

        public async Task<IEnumerable<Repuesto>> GetAll() =>
            await _db.Repuestos.AsNoTracking().ToListAsync();

        public async Task<Repuesto?> GetById(int id) =>
            await _db.Repuestos.FindAsync(id);

        public async Task Add(Repuesto repuesto)
        {
            await _db.Repuestos.AddAsync(repuesto);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Repuesto repuesto)
        {
            var existing = await _db.Repuestos.FindAsync(repuesto.Id)
                ?? throw new KeyNotFoundException($"Repuesto con ID {repuesto.Id} no encontrado.");
            _db.Entry(existing).CurrentValues.SetValues(repuesto);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Repuesto repuesto)
        {
            _db.Repuestos.Remove(repuesto);
            await _db.SaveChangesAsync();
        }
    }
}
