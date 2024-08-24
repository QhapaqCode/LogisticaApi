using Microsoft.AspNetCore.Mvc;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnaquelController : ControllerBase
    {
        private readonly IAnaquelService _anaquelService;

        public AnaquelController(IAnaquelService anaquelService)
        {
            _anaquelService = anaquelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var anaqueles = await _anaquelService.GetAllAnaquelesAsync();
            return Ok(anaqueles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var anaquel = await _anaquelService.GetAnaquelByIdAsync(id);
            if (anaquel == null)
            {
                return NotFound();
            }
            return Ok(anaquel);
        }

        [HttpGet("almacen/{almacenId}")]
        public async Task<IActionResult> GetByAlmacenId(int almacenId)
        {
            var anaqueles = await _anaquelService.GetAnaquelByAlmacenIdAsync(almacenId);
            return Ok(anaqueles);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Anaquel anaquel)
        {
            await _anaquelService.AddAnaquelAsync(anaquel);
            return CreatedAtAction(nameof(GetById), new { id = anaquel.Id }, anaquel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Anaquel anaquel)
        {
            if (id != anaquel.Id)
            {
                return BadRequest();
            }
            await _anaquelService.UpdateAnaquelAsync(anaquel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _anaquelService.DeleteAnaquelAsync(id);
            return NoContent();
        }
    }
}
