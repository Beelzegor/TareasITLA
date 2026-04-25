using Microsoft.AspNetCore.Mvc;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;

namespace Reparaciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly IReparacionRepository _repo;

        public ReparacionesController(IReparacionRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reparacion>>> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<Reparacion>> GetById(int id)
        {
            var reparacion = await _repo.GetById(id);
            return reparacion is null ? NotFound($"Reparación con ID {id} no encontrada.") : Ok(reparacion);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Reparacion>>> GetByCliente(int clienteId) =>
            Ok(await _repo.GetByCliente(clienteId));

        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<IEnumerable<Reparacion>>> GetByEstado(string estado) =>
            Ok(await _repo.GetByEstado(estado));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reparacion reparacion)
        {
            reparacion.FechaRecepcion = DateTime.UtcNow;
            await _repo.Add(reparacion);
            return CreatedAtAction(nameof(GetById), new { id = reparacion.Id }, reparacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Reparacion reparacion)
        {
            if (id != reparacion.Id) return BadRequest("El ID no coincide.");
            await _repo.Update(reparacion);
            return NoContent();
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var reparacion = await _repo.GetById(id);
            if (reparacion is null) return NotFound($"Reparación con ID {id} no encontrada.");
            reparacion.Estado = nuevoEstado;
            if (nuevoEstado == "Entregado") reparacion.FechaEntrega = DateTime.UtcNow;
            await _repo.Update(reparacion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reparacion = await _repo.GetById(id);
            if (reparacion is null) return NotFound($"Reparación con ID {id} no encontrada.");
            await _repo.Remove(reparacion);
            return NoContent();
        }
    }
}
