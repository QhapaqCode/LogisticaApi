using Microsoft.AspNetCore.Mvc;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {
        private readonly IAlmacenService _almacenService;

        public AlmacenController(IAlmacenService almacenService)
        {
            _almacenService = almacenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var almacenes = await _almacenService.GetAllAlmacenesAsync();
            return Ok(almacenes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var almacen = await _almacenService.GetAlmacenByIdAsync(id);
            if (almacen == null)
            {
                return NotFound();
            }
            return Ok(almacen);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Almacen almacen)
        {
            await _almacenService.AddAlmacenAsync(almacen);
            return CreatedAtAction(nameof(GetById), new { id = almacen.Id }, almacen);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Almacen almacen)
        {
            if (id != almacen.Id)
            {
                return BadRequest();
            }
            await _almacenService.UpdateAlmacenAsync(almacen);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _almacenService.DeleteAlmacenAsync(id);
            //return NoContent(); 111
            return NoContent();
        }
    }
}
