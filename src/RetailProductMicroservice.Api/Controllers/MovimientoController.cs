using Microsoft.AspNetCore.Mvc;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;

        public MovimientoController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimientos = await _movimientoService.GetAllMovimientosAsync();
            return Ok(movimientos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movimiento = await _movimientoService.GetMovimientoByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            return Ok(movimiento);
        }

        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var movimientos = await _movimientoService.GetMovimientosByDateRangeAsync(startDate, endDate);
            return Ok(movimientos);
        }

        [HttpGet("almacen/{almacenId}")]
        public async Task<IActionResult> GetByAlmacenId(int almacenId)
        {
            var movimientos = await _movimientoService.GetMovimientosByAlmacenAsync(almacenId);
            return Ok(movimientos);
        }

        [HttpGet("anaquel/{anaquelId}")]
        public async Task<IActionResult> GetByAnaquelId(int anaquelId)
        {
            var movimientos = await _movimientoService.GetMovimientosByAnaquelAsync(anaquelId);
            return Ok(movimientos);
        }

        [HttpGet("motivo/{motivo}")]
        public async Task<IActionResult> GetByMotivo(MotivoMovimiento motivo)
        {
            var movimientos = await _movimientoService.GetMovimientosByMotivoAsync(motivo);
            return Ok(movimientos);
        }

        [HttpGet("producto/{productoId}")]
        public async Task<IActionResult> GetByProductoId(int productoId)
        {
            var movimientos = await _movimientoService.GetMovimientosByProductoAsync(productoId);
            return Ok(movimientos);
        }

        [HttpGet("cantidad/{cantidad}")]
        public async Task<IActionResult> GetByCantidad(decimal cantidad)
        {
            var movimientos = await _movimientoService.GetMovimientosByCantidadAsync(cantidad);
            return Ok(movimientos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movimiento movimiento)
        {
            await _movimientoService.AddMovimientoAsync(movimiento);
            return CreatedAtAction(nameof(GetById), new { id = movimiento.Id }, movimiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Movimiento movimiento)
        {
            if (id != movimiento.Id)
            {
                return BadRequest();
            }
            await _movimientoService.UpdateMovimientoAsync(movimiento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movimientoService.DeleteMovimientoAsync(id);
            return NoContent();
        }
    }
}
