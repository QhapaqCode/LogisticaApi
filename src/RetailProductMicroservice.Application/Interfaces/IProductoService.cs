using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Application.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllProductsAsync();

    Task<IEnumerable<Producto>> GetProductsByAlmacenIdAsync(int almacenId);

    Task<IEnumerable<Producto>> GetProductsByAnaquelIdAsync(int anaquelId);

    Task<Producto> GetProductByIdAsync(int id);

    Task AddProductAsync(Producto producto);

    Task UpdateProductAsync(Producto producto);

    Task DeleteProductAsync(int id);
}
