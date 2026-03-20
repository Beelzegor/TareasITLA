using Microsoft.AspNetCore.Mvc;
using Productos.Server.Models;
using Productos.Domain.Interface;

namespace Productos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosRepository _repository;

        public ProductosController(IProductosRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            if (producto is null)
                return BadRequest("El producto no puede ser nulo.");

            await _repository.AddProducto(producto);
            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Producto>>> ListaProductos()
        {
            var productos = await _repository.GetAll();
            return Ok(productos);
        }

        [HttpGet]
        [Route("ver/{id}")]
        public async Task<ActionResult<Producto>> VerProducto(int id)
        {
            var producto = await _repository.GetProductobyID(id);

            if (producto is null)
                return NotFound($"Producto con ID {id} no encontrado.");

            return Ok(producto);
        }

        [HttpPut]
        [Route("editar/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (producto is null)
                return BadRequest("El producto no puede ser nulo.");

            var productoExistente = await _repository.GetProductobyID(id);

            if (productoExistente is null)
                return NotFound($"Producto con ID {id} no encontrado.");

            producto.Id = id;
            await _repository.EditarProducto(producto);
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _repository.GetProductobyID(id);

            if (producto is null)
                return NotFound($"Producto con ID {id} no encontrado.");

            await _repository.RemoveProducto(producto);
            return Ok();
        }
    }
}