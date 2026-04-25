using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;
using Reparaciones.Infrastructure.Context;

namespace Reparaciones.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ReparacionesContext _db;

        public ClienteRepository(ReparacionesContext db) => _db = db;

        public async Task<IEnumerable<Cliente>> GetAll() =>
            await _db.Clientes.AsNoTracking().ToListAsync();

        public async Task<Cliente?> GetById(int id) =>
            await _db.Clientes.FindAsync(id);

        public async Task Add(Cliente cliente)
        {
            await _db.Clientes.AddAsync(cliente);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Cliente cliente)
        {
            var existing = await _db.Clientes.FindAsync(cliente.Id)
                ?? throw new KeyNotFoundException($"Cliente con ID {cliente.Id} no encontrado.");
            _db.Entry(existing).CurrentValues.SetValues(cliente);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Cliente cliente)
        {
            _db.Clientes.Remove(cliente);
            await _db.SaveChangesAsync();
        }
    }
}
