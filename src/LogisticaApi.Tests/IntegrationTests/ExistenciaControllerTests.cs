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
    public class ExistenciaControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public ExistenciaControllerTests(WebApplicationFactory<Startup> factory)
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
            var existencia = new Existencia
            {
                Nombre = "Otra Existencia Test",
                Descripcion = "Otra Descripción Test",
                TipoProducto = TipoProducto.Individual,
                Marca = "Generico",
                EstadoEntidad = EstadoEntidad.Activo,

                Codigo = "EX124",
                UnidadMedida = UnidadMedida.Kilogramo
            };

            var content = new StringContent(JsonConvert.SerializeObject(existencia), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/existencia", content);
        }

        [Fact]
        public async Task GetExistencias_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/existencia");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetExistencia_ReturnsSuccessStatusCode()
        {
            var existenciaId = 1;
            var response = await _client.GetAsync($"/api/existencia/{existenciaId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetExistencia_ReturnsNotFoundStatusCode()
        {
            var existenciaId = 999;
            var response = await _client.GetAsync($"/api/existencia/{existenciaId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateExistencia_ReturnsSuccessStatusCode()
        {
            var existencia = new Existencia
            {
                Nombre = "Otra Existencia Test",
                Descripcion = "Otra Descripción Test",
                TipoProducto = TipoProducto.Individual,
                Marca = "Generico",
                EstadoEntidad = EstadoEntidad.Activo,

                Codigo = "EX124",
                UnidadMedida = UnidadMedida.Kilogramo
            };

            var content = new StringContent(JsonConvert.SerializeObject(existencia), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/existencia", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateExistencia_ReturnsSuccessStatusCode()
        {
            var existenciaId = 1;
            var existencia = new Existencia
            {
                Id = 1,
                Nombre = "Otra Existencia Test",
                Descripcion = "Otra Descripción Test",
                TipoProducto = TipoProducto.Accesorio,
                Marca = "Generico",
                EstadoEntidad = EstadoEntidad.Activo,

                Codigo = "EX124",
                UnidadMedida = UnidadMedida.Kilogramo
            };

            var content = new StringContent(JsonConvert.SerializeObject(existencia), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/existencia/{existenciaId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteExistencia_ReturnsSuccessStatusCode()
        {
            var existenciaId = 1;
            var response = await _client.DeleteAsync($"/api/existencia/{existenciaId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
