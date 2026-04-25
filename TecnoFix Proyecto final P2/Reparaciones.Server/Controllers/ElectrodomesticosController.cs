using Microsoft.AspNetCore.Mvc;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;

namespace Reparaciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectrodomesticosController : ControllerBase
    {
        private readonly IElectrodomesticoRepository _repo;

        public ElectrodomesticosController(IElectrodomesticoRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Electrodomestico>>> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<Electrodomestico>> GetById(int id)
        {
            var item = await _repo.GetById(id);
            return item is null ? NotFound($"Electrodoméstico con ID {id} no encontrado.") : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Electrodomestico electrodomestico)
        {
            await _repo.Add(electrodomestico);
            return CreatedAtAction(nameof(GetById), new { id = electrodomestico.Id }, electrodomestico);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Electrodomestico electrodomestico)
        {
            if (id != electrodomestico.Id) return BadRequest("El ID no coincide.");
            await _repo.Update(electrodomestico);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repo.GetById(id);
            if (item is null) return NotFound($"Electrodoméstico con ID {id} no encontrado.");
            await _repo.Remove(item);
            return NoContent();
        }
    }
}
