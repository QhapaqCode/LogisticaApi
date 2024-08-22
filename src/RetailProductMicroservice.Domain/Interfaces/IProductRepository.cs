using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}