using System.Collections.Generic;
using System.Threading.Tasks;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}