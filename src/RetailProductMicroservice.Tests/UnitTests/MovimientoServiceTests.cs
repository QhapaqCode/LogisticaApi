using Moq;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Application.Services;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using Xunit;

namespace RetailProductMicroservice.Tests.UnitTests
{
    public class MovimientoServiceTests
    {
        private readonly Mock<IMovimientoRepository> _movimientoRepositoryMock;
        private readonly IMovimientoService _movimientoService;

        public MovimientoServiceTests()
        {
            _movimientoRepositoryMock = new Mock<IMovimientoRepository>();
            _movimientoService = new MovimientoService(_movimientoRepositoryMock.Object);
        }

        [Fact]
        public async Task AddMovimientoAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var movimiento = new Movimiento
            {
                Id = 1,
                ProductoId = 1,
                AlmacenId = 1,
                Cantidad = 50,
                Direccion = DireccionMovimiento.Entrada, // Fix: Correct property name
                Fecha = DateTime.Now,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _movimientoRepositoryMock.Setup(repo => repo.AddMovimientoAsync(It.IsAny<Movimiento>())).Returns(Task.CompletedTask);

            // Act
            await _movimientoService.AddMovimientoAsync(movimiento);

            // Assert
            _movimientoRepositoryMock.Verify(repo => repo.AddMovimientoAsync(It.IsAny<Movimiento>()), Times.Once);
        }

        [Fact]
        public async Task GetMovimientoByIdAsync_ValidId_ReturnsMovimiento()
        {
            // Arrange
            var movimientoId = 1;
            var movimiento = new Movimiento
            {
                Id = movimientoId,
                ProductoId = 1,
                AlmacenId = 1,
                Cantidad = 50,
                Direccion = DireccionMovimiento.Entrada, // Fix: Correct property name
                Fecha = DateTime.Now,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _movimientoRepositoryMock.Setup(repo => repo.GetMovimientoByIdAsync(movimientoId)).ReturnsAsync(movimiento);

            // Act
            var result = await _movimientoService.GetMovimientoByIdAsync(movimientoId);

            // Assert
            Assert.Equal(movimiento, result);
        }

        [Fact]
        public async Task GetAllMovimientosAsync_ReturnsMovimientos()
        {
            // Arrange
            var movimientos = new List<Movimiento>
                {
                    new Movimiento
                    {
                        Id = 1,
                        ProductoId = 1,
                        AlmacenId = 1,
                        Cantidad = 50,
                        Direccion = DireccionMovimiento.Entrada, // Fix: Correct property name
                        Fecha = DateTime.Now,
                        EstadoEntidad = EstadoEntidad.Activo
                    },
                    new Movimiento
                    {
                        Id = 2,
                        ProductoId = 2,
                        AlmacenId = 1,
                        Cantidad = 30,
                        Direccion = DireccionMovimiento.Salida, // Fix: Correct property name
                        Fecha = DateTime.Now,
                        EstadoEntidad = EstadoEntidad.Activo
                    }
                };

            _movimientoRepositoryMock.Setup(repo => repo.GetAllMovimientosAsync()).ReturnsAsync(movimientos);

            // Act
            var result = await _movimientoService.GetAllMovimientosAsync();

            // Assert
            Assert.Equal(movimientos, result);
        }

        [Fact]
        public async Task UpdateMovimientoAsync_ValidData_UpdatesMovimiento()
        {
            // Arrange
            var movimiento = new Movimiento
            {
                Id = 1,
                ProductoId = 1,
                AlmacenId = 1,
                Cantidad = 60,
                Direccion = DireccionMovimiento.Entrada, // Fix: Correct property name
                Fecha = DateTime.Now,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _movimientoRepositoryMock.Setup(repo => repo.UpdateMovimientoAsync(It.IsAny<Movimiento>())).Returns(Task.CompletedTask);

            // Act
            await _movimientoService.UpdateMovimientoAsync(movimiento);

            // Assert
            _movimientoRepositoryMock.Verify(repo => repo.UpdateMovimientoAsync(It.IsAny<Movimiento>()), Times.Once);
        }

        [Fact]
        public async Task DeleteMovimientoAsync_ValidId_DeletesMovimiento()
        {
            // Arrange
            var movimientoId = 1;

            _movimientoRepositoryMock.Setup(repo => repo.DeleteMovimientoAsync(movimientoId)).Returns(Task.CompletedTask);

            // Act
            await _movimientoService.DeleteMovimientoAsync(movimientoId);

            // Assert
            _movimientoRepositoryMock.Verify(repo => repo.DeleteMovimientoAsync(movimientoId), Times.Once);
        }
    }
}
