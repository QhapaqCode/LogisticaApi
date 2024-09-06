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
    public class SerializableControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public SerializableControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetSerializables_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/serializables");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSerializable_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var serializableId = 1;
            var response = await client.GetAsync($"/api/serializables/{serializableId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSerializable_ReturnsNotFoundStatusCode()
        {
            var client = _factory.CreateClient();
            var serializableId = 999;
            var response = await client.GetAsync($"/api/serializables/{serializableId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateSerializable_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var serializable = new Serializable
            {
                Nombre = "Serializable Test",
                Descripcion = "Descripción Test",
                TipoProducto = TipoProducto.Individual,
                Codigo = "SER123",
                Serie = "SERIE123",
                EstadoProducto = EstadoProducto.Nuevo,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(serializable), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/serializables", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateSerializable_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var serializableId = 1;
            var serializable = new Serializable
            {
                Id = serializableId,
                Nombre = "Serializable Test Actualizado",
                Descripcion = "Descripción Test Actualizada",
                TipoProducto = TipoProducto.Accesorio,
                Codigo = "SER1234",
                Serie = "SERIE1234",
                EstadoProducto = EstadoProducto.Usado,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(serializable), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/serializables/{serializableId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSerializable_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var serializableId = 1;
            var response = await client.DeleteAsync($"/api/serializables/{serializableId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

