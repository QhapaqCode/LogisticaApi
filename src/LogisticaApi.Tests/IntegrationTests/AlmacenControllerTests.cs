using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
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
        private readonly HttpClient _client;

        public AlmacenControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true);
                });
            });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAlmacenes_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/almacen");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAlmacen_ReturnsSuccessStatusCode()
        {
            var almacenId = 1;
            var response = await _client.GetAsync($"/api/almacen/{almacenId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAlmacen_ReturnsNotFoundStatusCode()
        {
            var almacenId = 999;
            var response = await _client.GetAsync($"/api/almacen/{almacenId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateAlmacen_ReturnsSuccessStatusCode()
        {
            var almacen = new Almacen
            {
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(almacen), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/almacen", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAlmacen_ReturnsSuccessStatusCode()
        {
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
            var response = await _client.PutAsync($"/api/almacen/{almacenId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAlmacen_ReturnsSuccessStatusCode()
        {
            var almacenId = 1;
            var response = await _client.DeleteAsync($"/api/almacen/{almacenId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
