using Reparaciones.Domain.Entities;

namespace Reparaciones.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente?> GetById(int id);
        Task Add(Cliente cliente);
        Task Update(Cliente cliente);
        Task Remove(Cliente cliente);
    }
}
