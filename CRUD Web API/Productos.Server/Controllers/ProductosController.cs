using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Productos.Server.Models;

// Papadio esto es lo ultimo de lo ultimo, no cap
namespace Productos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosContext _context;

        public ProductosController(ProductosContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")] // Creacion del producto
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet] // Lista para poder ver los productos
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Producto>>> ListaProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        [HttpGet]
        [Route("ver")] // Aqui para ver el producto
        public async Task<ActionResult<Producto>> VerProducto(int id)
        {
            Producto producto = await _context.Productos.FindAsync(id); // Si esto aparece subrayado no es nada, funciona igual, cosas de la programacion
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }


        [HttpPut]
        [Route("editar")] // Aca para editar el producto
        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(id);

            productoExistente!.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;

            await _context.SaveChangesAsync();
            return Ok();

        }


        [HttpDelete]
        [Route("eliminar")] // Esto para eliminar el producto
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var productoBorrado = await _context.Productos.FindAsync(id);

            _context.Productos.Remove(productoBorrado!);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
