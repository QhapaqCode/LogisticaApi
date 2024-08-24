using Microsoft.AspNetCore.Mvc;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistenciaController : ControllerBase
    {
        private readonly IExistenciaService _existenciaService;

        public ExistenciaController(IExistenciaService existenciaService)
        {
            _existenciaService = existenciaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var existencias = await _existenciaService.GetAllExistenciasAsync();
            return Ok(existencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var existencia = await _existenciaService.GetExistenciaByIdAsync(id);
            if (existencia == null)
            {
                return NotFound();
            }
            return Ok(existencia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Existencia existencia)
        {
            await _existenciaService.AddExistenciaAsync(existencia);
            return CreatedAtAction(nameof(GetById), new { id = existencia.Id }, existencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Existencia existencia)
        {
            if (id != existencia.Id)
            {
                return BadRequest();
            }
            await _existenciaService.UpdateExistenciaAsync(existencia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _existenciaService.DeleteExistenciaAsync(id);
            return NoContent();
        }
    }
}
