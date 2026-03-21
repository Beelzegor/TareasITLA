using Microsoft.AspNetCore.Mvc;
using Productos.Application.Contract;
using Productos.Application.Dtos.Producto;
using System.ComponentModel.DataAnnotations;

namespace Productos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearProducto([FromBody] SaveProductoDto dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Ok("Producto creado correctamente.");
            }
            catch (ValidationException ex) { return BadRequest(ex.Message); }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ListaProductos()
        {
            try
            {
                var productos = await _service.GetAllAsync();
                return Ok(productos);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet]
        [Route("ver/{id}")]
        public async Task<ActionResult<ProductoDto>> VerProducto(int id)
        {
            try
            {
                var producto = await _service.GetByIdAsync(id);
                if (producto is null)
                    return NotFound($"Producto con ID {id} no encontrado.");
                return Ok(producto);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPut]
        [Route("editar/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] SaveProductoDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok("Producto actualizado correctamente.");
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (ValidationException ex) { return BadRequest(ex.Message); }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok("Producto eliminado correctamente.");
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}