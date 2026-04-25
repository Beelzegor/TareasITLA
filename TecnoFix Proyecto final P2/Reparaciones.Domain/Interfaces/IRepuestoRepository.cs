using Reparaciones.Domain.Entities;

namespace Reparaciones.Domain.Interfaces
{
    public interface IRepuestoRepository
    {
        Task<IEnumerable<Repuesto>> GetAll();
        Task<Repuesto?> GetById(int id);
        Task Add(Repuesto repuesto);
        Task Update(Repuesto repuesto);
        Task Remove(Repuesto repuesto);
    }
}
