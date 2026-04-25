using Microsoft.AspNetCore.Mvc;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;

namespace Reparaciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestosController : ControllerBase
    {
        private readonly IRepuestoRepository _repo;

        public RepuestosController(IRepuestoRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repuesto>>> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<Repuesto>> GetById(int id)
        {
            var repuesto = await _repo.GetById(id);
            return repuesto is null ? NotFound($"Repuesto con ID {id} no encontrado.") : Ok(repuesto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Repuesto repuesto)
        {
            await _repo.Add(repuesto);
            return CreatedAtAction(nameof(GetById), new { id = repuesto.Id }, repuesto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Repuesto repuesto)
        {
            if (id != repuesto.Id) return BadRequest("El ID no coincide.");
            await _repo.Update(repuesto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var repuesto = await _repo.GetById(id);
            if (repuesto is null) return NotFound($"Repuesto con ID {id} no encontrado.");
            await _repo.Remove(repuesto);
            return NoContent();
        }
    }
}
