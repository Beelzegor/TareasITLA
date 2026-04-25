using Reparaciones.Domain.Entities;

namespace Reparaciones.Domain.Interfaces
{
    public interface IReparacionRepository
    {
        Task<IEnumerable<Reparacion>> GetAll();
        Task<Reparacion?> GetById(int id);
        Task<IEnumerable<Reparacion>> GetByCliente(int clienteId);
        Task<IEnumerable<Reparacion>> GetByEstado(string estado);
        Task Add(Reparacion reparacion);
        Task Update(Reparacion reparacion);
        Task Remove(Reparacion reparacion);
    }
}
