using Xunit;
using Moq;
using System.Collections.Generic;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Tests.UnitTests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", CurrentStock = 10, Status = "Active" },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", CurrentStock = 5, Status = "Active" },
                new Product { Id = 3, Name = "Product 3", Description = "Description 3", CurrentStock = 0, Status = "Inactive" }
            };

            _productRepositoryMock.Setup(repo => repo.GetAllProducts()).Returns(expectedProducts);

            // Act
            var actualProducts = _productService.GetAllProducts();

            // Assert
            Assert.Equal(expectedProducts, actualProducts);
        }

        [Fact]
        public void GetProductById_ShouldReturnProductWithMatchingId()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { Id = productId, Name = "Product 1", Description = "Description 1", CurrentStock = 10, Status = "Active" };

            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).Returns(expectedProduct);

            // Act
            var actualProduct = _productService.GetProductById(productId);

            // Assert
            Assert.Equal(expectedProduct, actualProduct);
        }

        // Add more unit tests for other methods in ProductService class
    }
}