using Azure;
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
    [Collection("Test collection")]
    public class AnaquelControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;
        private static int _nextId = 1;
        private static readonly object _lock = new object();

        public AnaquelControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
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

        private async Task<int> InsertandoNuevoAnaquel()
        {
            int nuevoAnaquelId;
            lock (_lock)
                nuevoAnaquelId = _nextId++;

            var almacen = new Almacen
            {
                Id = nuevoAnaquelId,
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var anaquel = new Anaquel
            {
                Id = nuevoAnaquelId,
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = almacen.Id,
                EstadoEntidad = EstadoEntidad.Activo,
                Almacen = almacen
            };

            var content = new StringContent(JsonConvert.SerializeObject(anaquel), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/anaquel", content);

            return nuevoAnaquelId;
        }

        [Fact]
        public async Task GetAnaqueles_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/anaquel");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAnaquel_ReturnsSuccessStatusCode()
        {
            var anaquelId = await InsertandoNuevoAnaquel();
            var response = await _client.GetAsync($"/api/anaquel/{anaquelId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAnaquel_ReturnsNotFoundStatusCode()
        {
            var anaquelId = 999;
            var response = await _client.GetAsync($"/api/anaquel/{anaquelId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateAnaquel_ReturnsSuccessStatusCode()
        {
            var almacen = new Almacen
            {
                Id = 997,
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var anaquel = new Anaquel
            {
                Id = 997,
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = 998,
                EstadoEntidad = EstadoEntidad.Activo,
                Almacen = almacen
            };

            var content = new StringContent(JsonConvert.SerializeObject(anaquel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/anaquel", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAnaquel_ReturnsSuccessStatusCode()
        {
            var anaquelId = await InsertandoNuevoAnaquel();
            var almacen = new Almacen
            {
                Id = anaquelId,
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var anaquel = new Anaquel
            {
                Id = anaquelId,
                Codigo = "A1-Updated",
                Fila = 1,
                Columna = 1,
                AlmacenId = anaquelId,
                EstadoEntidad = EstadoEntidad.Activo,
                Almacen = almacen
            };
            var content = new StringContent(JsonConvert.SerializeObject(anaquel), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/anaquel/{anaquelId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAnaquel_ReturnsSuccessStatusCode()
        {
            var anaquelId = await InsertandoNuevoAnaquel();
            var response = await _client.DeleteAsync($"/api/anaquel/{anaquelId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
