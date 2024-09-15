using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
        private static readonly object _lock = new object();
        private static int _nextId = 1;

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
            InicializandoEntidades().Wait();
        }

        private async Task InicializandoEntidades()
        {
            int movimientoId;
            lock (_lock)
                movimientoId = _nextId++;

            var almacen = new Almacen
            {
                Id = 1,
                Nombre = "Almacen Test",
                Direccion = "Dirección Test",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };
            var anaquel = new Anaquel
            {
                Id = 1,
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
                EstadoEntidad = EstadoEntidad.Activo,
                Almacen = almacen
            };
            var existencia = new Existencia
            {
                Id = 1,
                Nombre = "Otra Existencia Test",
                Descripcion = "Otra Descripción Test",
                TipoProducto = TipoProducto.Individual,
                Marca = "Generico",
                EstadoEntidad = EstadoEntidad.Activo,
                Codigo = "EX124",
                UnidadMedida = UnidadMedida.Kilogramo
            };

            var almacenContent = new StringContent(JsonConvert.SerializeObject(almacen), Encoding.UTF8, "application/json");
            var anaquelContent = new StringContent(JsonConvert.SerializeObject(anaquel), Encoding.UTF8, "application/json");
            var existenciaContent = new StringContent(JsonConvert.SerializeObject(existencia), Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/almacen", almacenContent);
            await _client.PostAsync("/api/anaquel", anaquelContent);
            await _client.PostAsync("/api/existencia", existenciaContent);
        }

        private async Task<int> InsertandoMovimiento()
        {
            int nuevoMovimientoId;
            lock (_lock)
                nuevoMovimientoId = _nextId++;

            var movimiento = new Movimiento
            {
                Id = nuevoMovimientoId,
                Fecha = DateTime.UtcNow,
                Direccion = DireccionMovimiento.Entrada,
                Cantidad = 100,
                UnidadMedida = UnidadMedida.Kilogramo,
                AlmacenId = 1,
                Descripcion = "Movimiento Test",
                Motivo = MotivoMovimiento.Compra,
                AnaquelId = 1,
                ProductoId = 1,
                EstadoEntidad = EstadoEntidad.Activo
            };

            var movimientoContent = new StringContent(JsonConvert.SerializeObject(movimiento), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/movimiento", movimientoContent);
            return nuevoMovimientoId;
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
            var movimientoId = await InsertandoMovimiento();
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
            int nuevoMovimientoId;
            lock (_lock)
                nuevoMovimientoId = _nextId++;

            var movimiento = new Movimiento
            {

                Fecha = DateTime.UtcNow,
                Direccion = DireccionMovimiento.Entrada,
                Cantidad = 100,
                UnidadMedida = UnidadMedida.Kilogramo,
                AlmacenId = 1,
                Descripcion = "Movimiento Test",
                Motivo = MotivoMovimiento.Compra,
                AnaquelId = 1,
                ProductoId = 1,
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
            var movimientoId = await InsertandoMovimiento();
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
            var movimientoId = await InsertandoMovimiento();
            var response = await _client.DeleteAsync($"/api/movimiento/{movimientoId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
