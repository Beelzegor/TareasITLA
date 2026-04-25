using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;
using Reparaciones.Infrastructure.Context;

namespace Reparaciones.Infrastructure.Repositories
{
    public class TecnicoRepository : ITecnicoRepository
    {
        private readonly ReparacionesContext _db;

        public TecnicoRepository(ReparacionesContext db) => _db = db;

        public async Task<IEnumerable<Tecnico>> GetAll() =>
            await _db.Tecnicos.AsNoTracking().ToListAsync();

        public async Task<Tecnico?> GetById(int id) =>
            await _db.Tecnicos.FindAsync(id);

        public async Task Add(Tecnico tecnico)
        {
            await _db.Tecnicos.AddAsync(tecnico);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Tecnico tecnico)
        {
            var existing = await _db.Tecnicos.FindAsync(tecnico.Id)
                ?? throw new KeyNotFoundException($"Técnico con ID {tecnico.Id} no encontrado.");
            _db.Entry(existing).CurrentValues.SetValues(tecnico);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Tecnico tecnico)
        {
            _db.Tecnicos.Remove(tecnico);
            await _db.SaveChangesAsync();
        }
    }
}
