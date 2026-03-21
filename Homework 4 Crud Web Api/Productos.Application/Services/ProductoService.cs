using Productos.Application.Contract;
using Productos.Application.Dtos.Producto;
using Productos.Domain.Interface;
using Productos.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Productos.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductosRepository _repository;

        public ProductoService(IProductosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductoDto>> GetAllAsync()
        {
            var productos = await _repository.GetAll();

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio
            });
        }

        public async Task<ProductoDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número mayor a 0.");

            var producto = await _repository.GetProductobyID(id);

            if (producto is null)
                return null;

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio
            };
        }

        public async Task CreateAsync(SaveProductoDto dto)
        {
            ValidarDto(dto);

            var producto = new Producto
            {
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion.Trim(),
                Precio = dto.Precio
            };

            await _repository.AddProducto(producto);
        }

        public async Task UpdateAsync(int id, SaveProductoDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número mayor a 0.");

            ValidarDto(dto);

            var existente = await _repository.GetProductobyID(id)
                ?? throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            existente.Nombre = dto.Nombre.Trim();
            existente.Descripcion = dto.Descripcion.Trim();
            existente.Precio = dto.Precio;

            await _repository.EditarProducto(existente);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número mayor a 0.");

            var existente = await _repository.GetProductobyID(id)
                ?? throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            await _repository.RemoveProducto(existente);
        }

        private static void ValidarDto(SaveProductoDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Los datos del producto no pueden ser nulos.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ValidationException("El campo Nombre es obligatorio.");

            if (dto.Nombre.Trim().Length > 50)
                throw new ValidationException("El campo Nombre debe tener máximo 50 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ValidationException("El campo Descripcion es obligatorio.");

            if (dto.Descripcion.Trim().Length > 500)
                throw new ValidationException("El campo Descripcion debe tener máximo 500 caracteres.");

            if (dto.Precio <= 0)
                throw new ValidationException("El campo Precio debe ser mayor a 0.");

            if (dto.Precio > 999999.99m)
                throw new ValidationException("El campo Precio no puede superar 999,999.99.");
        }
    }
}
