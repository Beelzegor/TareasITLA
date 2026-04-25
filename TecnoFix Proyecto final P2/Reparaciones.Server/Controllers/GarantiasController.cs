using Microsoft.AspNetCore.Mvc;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;

namespace Reparaciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarantiasController : ControllerBase
    {
        private readonly IGarantiaRepository _repo;

        public GarantiasController(IGarantiaRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Garantia>>> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<Garantia>> GetById(int id)
        {
            var garantia = await _repo.GetById(id);
            return garantia is null ? NotFound($"Garantía con ID {id} no encontrada.") : Ok(garantia);
        }

        [HttpGet("reparacion/{reparacionId}")]
        public async Task<ActionResult<Garantia>> GetByReparacion(int reparacionId)
        {
            var garantia = await _repo.GetByReparacion(reparacionId);
            return garantia is null ? NotFound("No hay garantía para esta reparación.") : Ok(garantia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Garantia garantia)
        {
            await _repo.Add(garantia);
            return CreatedAtAction(nameof(GetById), new { id = garantia.Id }, garantia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Garantia garantia)
        {
            if (id != garantia.Id) return BadRequest("El ID no coincide.");
            await _repo.Update(garantia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var garantia = await _repo.GetById(id);
            if (garantia is null) return NotFound($"Garantía con ID {id} no encontrada.");
            await _repo.Remove(garantia);
            return NoContent();
        }
    }
}
