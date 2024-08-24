using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RetailProductMicroservice.Api;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.ValueObjects;
using System.Net;
using System.Text;
using Xunit;

namespace RetailProductMicroservice.Tests.IntegrationTests
{
    public class AlmacenControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AlmacenControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAlmacenes_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/almacenes");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAlmacen_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var almacenId = 1;

            // Act
            var response = await client.GetAsync($"/api/almacenes/{almacenId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAlmacen_ReturnsNotFoundStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var almacenId = 999;

            // Act
            var response = await client.GetAsync($"/api/almacenes/{almacenId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateAlmacen_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var almacen = new Almacen
            {
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(almacen), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/almacenes", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAlmacen_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var almacenId = 1;
            var almacen = new Almacen
            {
                Id = almacenId,
                Nombre = "Almacen Test Actualizado",
                Direccion = "Dirección Test Actualizada",
                TipoAlmacen = TipoAlmacen.Tienda,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(almacen), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync($"/api/almacenes/{almacenId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAlmacen_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var almacenId = 1;

            // Act
            var response = await client.DeleteAsync($"/api/almacenes/{almacenId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
