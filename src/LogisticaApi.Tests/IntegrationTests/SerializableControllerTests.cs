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
    public class SerializableControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private static readonly object _lock = new object();
        private static int _nextId = 1;

        public SerializableControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
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

        private async Task<int> InsertandoSerializable()
        {
            int newSerializableId;
            lock (_lock)
                newSerializableId = _nextId++;

            var serializable = new Serializable
            {
                Id = newSerializableId,
                Nombre = "Serializable Test",
                Descripcion = "Descripción Test",
                TipoProducto = TipoProducto.Individual,
                Marca = "SER123",
                Serie = "SERIE123",
                EstadoProducto = EstadoProducto.Nuevo,
                EstadoEntidad = EstadoEntidad.Activo
            };

            var content = new StringContent(JsonConvert.SerializeObject(serializable), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/serializable", content);

            return newSerializableId;
        }

        [Fact]
        public async Task Getserializable_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/serializable");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private readonly ITestOutputHelper _testOutputHelper;

        [Fact]
        public async Task GetSerializable_ReturnsSuccessStatusCode()
        {
            var serializableId = await InsertandoSerializable();
            var response = await _client.GetAsync($"/api/serializable/{serializableId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSerializable_ReturnsNotFoundStatusCode()
        {
            var serializableId = 999;
            var response = await _client.GetAsync($"/api/serializable/{serializableId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateSerializable_ReturnsSuccessStatusCode()
        {
            int serializableId;
            lock (_lock)
                serializableId = _nextId++;

            var serializable = new Serializable
            {
                Id = serializableId,
                Nombre = "Serializable Test",
                Descripcion = "Descripción Test",
                TipoProducto = TipoProducto.Individual,
                Marca = "SER123",
                Serie = "SERIE123",
                EstadoProducto = EstadoProducto.Nuevo,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(serializable), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/serializable", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateSerializable_ReturnsSuccessStatusCode()
        {
            var serializableId = await InsertandoSerializable();
            var serializable = new Serializable
            {
                Id = serializableId,
                Nombre = "Serializable Test Actualizado",
                Descripcion = "Descripción Test Actualizada",
                TipoProducto = TipoProducto.Accesorio,
                Marca = "SER1234",
                Serie = "SERIE1234",
                EstadoProducto = EstadoProducto.Usado,
                EstadoEntidad = EstadoEntidad.Activo
            };

            var content = new StringContent(JsonConvert.SerializeObject(serializable), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/serializable/{serializableId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSerializable_ReturnsSuccessStatusCode()
        {
            var serializableId = await InsertandoSerializable();
            var response = await _client.DeleteAsync($"/api/serializable/{serializableId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

