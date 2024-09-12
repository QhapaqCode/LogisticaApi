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
    [Collection("Test collection")]
    public class AnaquelControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public AnaquelControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true);
                });
            });
            _client = _factory.CreateClient();
            InitializeDatabaseAsync().Wait();
        }

        private async Task InitializeDatabaseAsync()
        {
            var almacen = new Almacen
            {
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var anaquel = new Anaquel
            {
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
                EstadoEntidad = EstadoEntidad.Activo,
                Almacen = almacen
            };

            var content = new StringContent(JsonConvert.SerializeObject(anaquel), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/anaquel", content);
        }

        [Fact]
        public async Task GetAnaqueles_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/anaquel");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //[Fact]
        //public async Task GetAnaquel_ReturnsSuccessStatusCode()
        //{
        //    var anaquelId = 1;
        //    var response = await _client.GetAsync($"/api/anaquel/{anaquelId}");
        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}

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
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var anaquel = new Anaquel
            {
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
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
            var almacen = new Almacen
            {
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var anaquelId = 1;
            var anaquel = new Anaquel
            {
                Id = anaquelId,
                Codigo = "A1-Updated",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
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
            var anaquelId = 1;
            var response = await _client.DeleteAsync($"/api/anaquel/{anaquelId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
