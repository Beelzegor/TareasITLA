using Reparaciones.Domain.Entities;

namespace Reparaciones.Domain.Interfaces
{
    public interface ITecnicoRepository
    {
        Task<IEnumerable<Tecnico>> GetAll();
        Task<Tecnico?> GetById(int id);
        Task Add(Tecnico tecnico);
        Task Update(Tecnico tecnico);
        Task Remove(Tecnico tecnico);
    }
}
