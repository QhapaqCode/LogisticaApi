using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RetailProductMicroservice.Api;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.ValueObjects;
using System.Net;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace RetailProductMicroservice.Tests.IntegrationTests
{
    public class AlmacenControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private static readonly object _lock = new object();
        private static int _nextId = 1;
        private readonly ITestOutputHelper _testOutputHelper;

        public AlmacenControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true);
                });
            });
            _client = _factory.CreateClient();
            _testOutputHelper = testOutputHelper;
        }

        private async Task<int> InsertandoNuevoAlmacen()
        {
            int nuevoAlmacenId;
            lock (_lock)
                nuevoAlmacenId = _nextId++;

            var almacen = new Almacen
            {
                Id = nuevoAlmacenId,
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };

            var content = new StringContent(JsonConvert.SerializeObject(almacen), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/almacen", content);

            return nuevoAlmacenId;
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
            var almacenId = await InsertandoNuevoAlmacen();
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
                Id = 998,
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(almacen), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/almacen", content);
            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAlmacen_ReturnsSuccessStatusCode()
        {
            var almacenId = await InsertandoNuevoAlmacen();
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
            var almacenId = await InsertandoNuevoAlmacen();
            var response = await _client.DeleteAsync($"/api/almacen/{almacenId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
