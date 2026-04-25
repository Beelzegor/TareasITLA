using Reparaciones.Domain.Entities;

namespace Reparaciones.Domain.Interfaces
{
    public interface IGarantiaRepository
    {
        Task<IEnumerable<Garantia>> GetAll();
        Task<Garantia?> GetById(int id);
        Task<Garantia?> GetByReparacion(int reparacionId);
        Task Add(Garantia garantia);
        Task Update(Garantia garantia);
        Task Remove(Garantia garantia);
    }
}
