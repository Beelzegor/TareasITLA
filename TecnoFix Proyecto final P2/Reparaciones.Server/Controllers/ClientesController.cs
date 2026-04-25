using Microsoft.AspNetCore.Mvc;
using Reparaciones.Domain.Entities;
using Reparaciones.Domain.Interfaces;

namespace Reparaciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repo;

        public ClientesController(IClienteRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var cliente = await _repo.GetById(id);
            return cliente is null ? NotFound($"Cliente con ID {id} no encontrado.") : Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            await _repo.Add(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest("El ID no coincide.");
            await _repo.Update(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _repo.GetById(id);
            if (cliente is null) return NotFound($"Cliente con ID {id} no encontrado.");
            await _repo.Remove(cliente);
            return NoContent();
        }
    }
}
