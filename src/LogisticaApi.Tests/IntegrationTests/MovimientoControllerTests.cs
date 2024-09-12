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
    public class MovimientoControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public MovimientoControllerTests(WebApplicationFactory<Startup> factory)
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
            var existencia = new Existencia
            {
                Nombre = "Existencia Test",
                Descripcion = "Descripción Test",
                Codigo = "EX123",
                UnidadMedida = UnidadMedida.Kilogramo,
                EstadoEntidad = EstadoEntidad.Activo,
                TipoProducto = TipoProducto.Individual
            };
            var movimiento = new Movimiento
            {
                Fecha = DateTime.UtcNow,
                Direccion = DireccionMovimiento.Entrada,
                Cantidad = 100,
                UnidadMedida = UnidadMedida.Kilogramo,
                AlmacenId = 1,
                Descripcion = "Movimiento Test",
                Motivo = MotivoMovimiento.Compra,
                ProductoId = 1,
                EstadoEntidad = EstadoEntidad.Activo,
                Almacen = almacen,
                Anaquel = anaquel,
                Producto = existencia
            };
            var content = new StringContent(JsonConvert.SerializeObject(movimiento), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/movimiento", content);
        }

        [Fact]
        public async Task GetMovimientos_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/movimiento");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMovimiento_ReturnsSuccessStatusCode()
        {
            var movimientoId = 1;
            var response = await _client.GetAsync($"/api/movimiento/{movimientoId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMovimiento_ReturnsNotFoundStatusCode()
        {
            var movimientoId = 999;
            var response = await _client.GetAsync($"/api/movimiento/{movimientoId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateMovimiento_ReturnsSuccessStatusCode()
        {
            var movimiento = new Movimiento
            {
                Fecha = DateTime.UtcNow,
                Direccion = DireccionMovimiento.Entrada,
                Cantidad = 100,
                UnidadMedida = UnidadMedida.Kilogramo,
                AlmacenId = 2,
                Descripcion = "Otro Movimiento Test",
                Motivo = MotivoMovimiento.Compra,
                ProductoId = 2,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(movimiento), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/movimiento", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateMovimiento_ReturnsSuccessStatusCode()
        {
            var movimientoId = 1;
            var movimiento = new Movimiento
            {
                Id = movimientoId,
                Fecha = DateTime.UtcNow,
                Direccion = DireccionMovimiento.Salida,
                Cantidad = 50,
                UnidadMedida = UnidadMedida.Litro,
                AlmacenId = 1,
                Descripcion = "Movimiento Test Actualizado",
                Motivo = MotivoMovimiento.Venta,
                ProductoId = 1,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(movimiento), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/movimiento/{movimientoId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMovimiento_ReturnsSuccessStatusCode()
        {
            var movimientoId = 1;
            var response = await _client.DeleteAsync($"/api/movimiento/{movimientoId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
