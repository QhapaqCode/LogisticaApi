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
    public class ExistenciaControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ExistenciaControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetExistencias_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/existencias");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetExistencia_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var existenciaId = 1;
            var response = await client.GetAsync($"/api/existencias/{existenciaId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetExistencia_ReturnsNotFoundStatusCode()
        {
            var client = _factory.CreateClient();
            var existenciaId = 999;
            var response = await client.GetAsync($"/api/existencias/{existenciaId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateExistencia_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var existencia = new Existencia
            {
                Nombre = "Existencia Test",
                Descripcion = "Descripción Test",
                Codigo = "EX123",
                UnidadMedida = UnidadMedida.Kilogramo,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(existencia), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/existencias", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateExistencia_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var existenciaId = 1;
            var existencia = new Existencia
            {
                Id = existenciaId,
                Nombre = "Existencia Test Actualizada",
                Descripcion = "Descripción Test Actualizada",
                Codigo = "EX1234",
                UnidadMedida = UnidadMedida.Litro,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(existencia), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/existencias/{existenciaId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteExistencia_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var existenciaId = 1;
            var response = await client.DeleteAsync($"/api/existencias/{existenciaId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
