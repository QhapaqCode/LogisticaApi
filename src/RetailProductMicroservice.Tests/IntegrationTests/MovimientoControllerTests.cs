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
    public class MovimientoControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MovimientoControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetMovimientos_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/movimientos");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMovimiento_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var movimientoId = 1;
            var response = await client.GetAsync($"/api/movimientos/{movimientoId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMovimiento_ReturnsNotFoundStatusCode()
        {
            var client = _factory.CreateClient();
            var movimientoId = 999;
            var response = await client.GetAsync($"/api/movimientos/{movimientoId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateMovimiento_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
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
                EstadoEntidad = EstadoEntidad.Activo
            };
            var content = new StringContent(JsonConvert.SerializeObject(movimiento), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/movimientos", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateMovimiento_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
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
            var response = await client.PutAsync($"/api/movimientos/{movimientoId}", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMovimiento_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var movimientoId = 1;
            var response = await client.DeleteAsync($"/api/movimientos/{movimientoId}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
