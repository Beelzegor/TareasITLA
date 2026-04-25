using Microsoft.AspNetCore.Mvc;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;

namespace Reparaciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicosController : ControllerBase
    {
        private readonly ITecnicoRepository _repo;

        public TecnicosController(ITecnicoRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tecnico>>> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<Tecnico>> GetById(int id)
        {
            var tecnico = await _repo.GetById(id);
            return tecnico is null ? NotFound($"Técnico con ID {id} no encontrado.") : Ok(tecnico);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tecnico tecnico)
        {
            await _repo.Add(tecnico);
            return CreatedAtAction(nameof(GetById), new { id = tecnico.Id }, tecnico);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tecnico tecnico)
        {
            if (id != tecnico.Id) return BadRequest("El ID no coincide.");
            await _repo.Update(tecnico);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tecnico = await _repo.GetById(id);
            if (tecnico is null) return NotFound($"Técnico con ID {id} no encontrado.");
            await _repo.Remove(tecnico);
            return NoContent();
        }
    }
}
