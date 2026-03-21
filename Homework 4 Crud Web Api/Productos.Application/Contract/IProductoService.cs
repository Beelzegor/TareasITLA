using Productos.Application.Dtos.Producto;

namespace Productos.Application.Contract
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> GetAllAsync();
        Task<ProductoDto?> GetByIdAsync(int id);
        Task CreateAsync(SaveProductoDto dto);
        Task UpdateAsync(int id, SaveProductoDto dto);
        Task DeleteAsync(int id);
    }
}
