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
    public class AnaquelControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AnaquelControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAnaqueles_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/anaqueles");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAnaquel_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var anaquelId = 1;
            var response = await client.GetAsync($"/api/anaqueles/{anaquelId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAnaquel_ReturnsNotFoundStatusCode()
        {
            var client = _factory.CreateClient();
            var anaquelId = 999;
            var response = await client.GetAsync($"/api/anaqueles/{anaquelId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateAnaquel_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var anaquel = new Anaquel
            {
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(anaquel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/anaqueles", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAnaquel_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var anaquelId = 1;
            var anaquel = new Anaquel
            {
                Id = anaquelId,
                Codigo = "A1-Updated",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(anaquel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/anaqueles/{anaquelId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAnaquel_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var anaquelId = 1;
            var response = await client.DeleteAsync($"/api/anaqueles/{anaquelId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
