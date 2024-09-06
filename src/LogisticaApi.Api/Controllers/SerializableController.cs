using Microsoft.AspNetCore.Mvc;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerializableController : ControllerBase
    {
        private readonly ISerializableService _serializableService;

        public SerializableController(ISerializableService serializableService)
        {
            _serializableService = serializableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serializables = await _serializableService.GetAllSerializablesAsync();
            return Ok(serializables);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serializable = await _serializableService.GetSerializableByIdAsync(id);
            if (serializable == null)
            {
                return NotFound();
            }
            return Ok(serializable);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Serializable serializable)
        {
            await _serializableService.AddSerializableAsync(serializable);
            return CreatedAtAction(nameof(GetById), new { id = serializable.Id }, serializable);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Serializable serializable)
        {
            if (id != serializable.Id)
            {
                return BadRequest();
            }
            await _serializableService.UpdateSerializableAsync(serializable);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // test 123
            await _serializableService.DeleteSerializableAsync(id);
            return NoContent();
        }
    }
}
