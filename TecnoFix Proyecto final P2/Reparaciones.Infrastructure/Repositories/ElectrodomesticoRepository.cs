using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;
using Reparaciones.Infrastructure.Context;

namespace Reparaciones.Infrastructure.Repositories
{
    public class ElectrodomesticoRepository : IElectrodomesticoRepository
    {
        private readonly ReparacionesContext _db;

        public ElectrodomesticoRepository(ReparacionesContext db) => _db = db;

        public async Task<IEnumerable<Electrodomestico>> GetAll() =>
            await _db.Electrodomesticos.AsNoTracking().ToListAsync();

        public async Task<Electrodomestico?> GetById(int id) =>
            await _db.Electrodomesticos.FindAsync(id);

        public async Task Add(Electrodomestico electrodomestico)
        {
            await _db.Electrodomesticos.AddAsync(electrodomestico);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Electrodomestico electrodomestico)
        {
            var existing = await _db.Electrodomesticos.FindAsync(electrodomestico.Id)
                ?? throw new KeyNotFoundException($"Electrodoméstico con ID {electrodomestico.Id} no encontrado.");
            _db.Entry(existing).CurrentValues.SetValues(electrodomestico);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Electrodomestico electrodomestico)
        {
            _db.Electrodomesticos.Remove(electrodomestico);
            await _db.SaveChangesAsync();
        }
    }
}
