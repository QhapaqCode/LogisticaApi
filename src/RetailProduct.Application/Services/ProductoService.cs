using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;

namespace RetailProductMicroservice.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<Producto>> GetAllProductsAsync()
        {
            return await _productoRepository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<Producto>> GetProductsByAlmacenIdAsync(int almacenId)
        {
            return await _productoRepository.GetProductsByAlmacenIdAsync(almacenId);
        }

        public async Task<IEnumerable<Producto>> GetProductsByAnaquelIdAsync(int anaquelId)
        {
            return await _productoRepository.GetProductsByAnaquelIdAsync(anaquelId);
        }

        public async Task<Producto> GetProductByIdAsync(int id)
        {
            return await _productoRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(Producto producto)
        {
            await _productoRepository.AddProductAsync(producto);
        }

        public async Task UpdateProductAsync(Producto producto)
        {
            await _productoRepository.UpdateProductAsync(producto);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productoRepository.DeleteProductAsync(id);
        }
    }
}
