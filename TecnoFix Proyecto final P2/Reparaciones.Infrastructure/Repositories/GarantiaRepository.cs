using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;
using Reparaciones.Infrastructure.Context;

namespace Reparaciones.Infrastructure.Repositories
{
    public class GarantiaRepository : IGarantiaRepository
    {
        private readonly ReparacionesContext _db;

        public GarantiaRepository(ReparacionesContext db) => _db = db;

        public async Task<IEnumerable<Garantia>> GetAll() =>
            await _db.Garantias.AsNoTracking().Include(g => g.Reparacion).ToListAsync();

        public async Task<Garantia?> GetById(int id) =>
            await _db.Garantias.Include(g => g.Reparacion).FirstOrDefaultAsync(g => g.Id == id);

        public async Task<Garantia?> GetByReparacion(int reparacionId) =>
            await _db.Garantias.FirstOrDefaultAsync(g => g.ReparacionId == reparacionId);

        public async Task Add(Garantia garantia)
        {
            await _db.Garantias.AddAsync(garantia);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Garantia garantia)
        {
            var existing = await _db.Garantias.FindAsync(garantia.Id)
                ?? throw new KeyNotFoundException($"Garantía con ID {garantia.Id} no encontrada.");
            _db.Entry(existing).CurrentValues.SetValues(garantia);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Garantia garantia)
        {
            _db.Garantias.Remove(garantia);
            await _db.SaveChangesAsync();
        }
    }
}
