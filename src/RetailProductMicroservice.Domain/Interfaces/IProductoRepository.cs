using RetailProductMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RetailProductMicroservice.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllProductsAsync();

        Task<IEnumerable<Producto>> GetProductsByAlmacenIdAsync(int almacenId);

        Task<IEnumerable<Producto>> GetProductsByAnaquelIdAsync(int anaquelId);

        Task<Producto> GetProductByIdAsync(int id);

        Task AddProductAsync(Producto producto);

        Task UpdateProductAsync(Producto producto);

        Task DeleteProductAsync(int id);
    }
}
