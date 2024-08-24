using Moq;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Application.Services;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using Xunit;

namespace RetailProductMicroservice.Tests.UnitTests
{
    public class AlmacenServiceTests
    {
        private readonly Mock<IAlmacenRepository> _almacenRepositoryMock;
        private readonly IAlmacenService _almacenService;

        public AlmacenServiceTests()
        {
            _almacenRepositoryMock = new Mock<IAlmacenRepository>();
            _almacenService = new AlmacenService(_almacenRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAlmacenAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var almacen = new Almacen
            {
                Id = 1,
                Nombre = "Almacen Central",
                Direccion = "Calle Falsa 123",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _almacenRepositoryMock.Setup(repo => repo.AddAlmacenAsync(It.IsAny<Almacen>())).Returns(Task.CompletedTask);

            // Act
            await _almacenService.AddAlmacenAsync(almacen);

            // Assert
            _almacenRepositoryMock.Verify(repo => repo.AddAlmacenAsync(It.IsAny<Almacen>()), Times.Once);
        }

        [Fact]
        public async Task GetAlmacenByIdAsync_ValidId_ReturnsAlmacen()
        {
            // Arrange
            var almacenId = 1;
            var almacen = new Almacen
            {
                Id = almacenId,
                Nombre = "Almacen Central",
                Direccion = "Calle Falsa 123",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _almacenRepositoryMock.Setup(repo => repo.GetAlmacenByIdAsync(almacenId)).ReturnsAsync(almacen);

            // Act
            var result = await _almacenService.GetAlmacenByIdAsync(almacenId);

            // Assert
            Assert.Equal(almacen, result);
        }

        [Fact]
        public async Task GetAllAlmacenesAsync_ReturnsAlmacenes()
        {
            // Arrange
            var almacenes = new List<Almacen>
            {
                new Almacen
                {
                    Id = 1,
                    Nombre = "Almacen Central",
                    Direccion = "Calle Falsa 123",
                    TipoAlmacen = TipoAlmacen.Almacen,
                    EstadoEntidad = EstadoEntidad.Activo
                },
                new Almacen
                {
                    Id = 2,
                    Nombre = "Almacen Secundario",
                    Direccion = "Avenida Siempre Viva 742",
                    TipoAlmacen = TipoAlmacen.Bodega,
                    EstadoEntidad = EstadoEntidad.Activo
                }
            };

            _almacenRepositoryMock.Setup(repo => repo.GetAllAlmacenesAsync()).ReturnsAsync(almacenes);

            // Act
            var result = await _almacenService.GetAllAlmacenesAsync();

            // Assert
            Assert.Equal(almacenes, result);
        }

        [Fact]
        public async Task UpdateAlmacenAsync_ValidData_UpdatesAlmacen()
        {
            // Arrange
            var almacen = new Almacen
            {
                Id = 1,
                Nombre = "Almacen Central Actualizado",
                Direccion = "Calle Falsa 123",
                TipoAlmacen = TipoAlmacen.Almacen,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _almacenRepositoryMock.Setup(repo => repo.UpdateAlmacenAsync(It.IsAny<Almacen>())).Returns(Task.CompletedTask);

            // Act
            await _almacenService.UpdateAlmacenAsync(almacen);

            // Assert
            _almacenRepositoryMock.Verify(repo => repo.UpdateAlmacenAsync(It.IsAny<Almacen>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAlmacenAsync_ValidId_DeletesAlmacen()
        {
            // Arrange
            var almacenId = 1;

            _almacenRepositoryMock.Setup(repo => repo.DeleteAlmacenAsync(almacenId)).Returns(Task.CompletedTask);

            // Act
            await _almacenService.DeleteAlmacenAsync(almacenId);

            // Assert
            _almacenRepositoryMock.Verify(repo => repo.DeleteAlmacenAsync(almacenId), Times.Once);
        }
    }
}
