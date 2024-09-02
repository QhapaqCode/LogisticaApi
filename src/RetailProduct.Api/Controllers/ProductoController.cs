using Microsoft.AspNetCore.Mvc;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.GetAllProductsAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.GetProductByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            await _productoService.AddProductAsync(producto);
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }
            await _productoService.UpdateProductAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productoService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}

