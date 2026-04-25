using Reparaciones.Domain.Entities;

namespace Reparaciones.Domain.Interfaces
{
    public interface IElectrodomesticoRepository
    {
        Task<IEnumerable<Electrodomestico>> GetAll();
        Task<Electrodomestico?> GetById(int id);
        Task Add(Electrodomestico electrodomestico);
        Task Update(Electrodomestico electrodomestico);
        Task Remove(Electrodomestico electrodomestico);
    }
}
