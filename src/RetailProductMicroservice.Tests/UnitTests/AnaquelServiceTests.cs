using Moq;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Application.Services;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using Xunit;

namespace RetailProductMicroservice.Tests.UnitTests
{
    public class AnaquelServiceTests
    {
        private readonly Mock<IAnaquelRepository> _anaquelRepositoryMock;
        private readonly IAnaquelService _anaquelService;

        public AnaquelServiceTests()
        {
            _anaquelRepositoryMock = new Mock<IAnaquelRepository>();
            _anaquelService = new AnaquelService(_anaquelRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAnaquelAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var anaquel = new Anaquel
            {
                Id = 1,
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _anaquelRepositoryMock.Setup(repo => repo.AddAnaquelAsync(It.IsAny<Anaquel>())).Returns(Task.CompletedTask);

            // Act
            await _anaquelService.AddAnaquelAsync(anaquel);

            // Assert
            _anaquelRepositoryMock.Verify(repo => repo.AddAnaquelAsync(It.IsAny<Anaquel>()), Times.Once);
        }

        [Fact]
        public async Task GetAnaquelByIdAsync_ValidId_ReturnsAnaquel()
        {
            // Arrange
            var anaquelId = 1;
            var anaquel = new Anaquel
            {
                Id = anaquelId,
                Codigo = "A1",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _anaquelRepositoryMock.Setup(repo => repo.GetAnaquelByIdAsync(anaquelId)).ReturnsAsync(anaquel);

            // Act
            var result = await _anaquelService.GetAnaquelByIdAsync(anaquelId);

            // Assert
            Assert.Equal(anaquel, result);
        }

        [Fact]
        public async Task GetAllAnaquelesAsync_ReturnsAnaqueles()
        {
            // Arrange
            var anaqueles = new List<Anaquel>
                {
                    new Anaquel
                    {
                        Id = 1,
                        Codigo = "A1",
                        Fila = 1,
                        Columna = 1,
                        AlmacenId = 1,
                        EstadoEntidad = EstadoEntidad.Activo
                    },
                    new Anaquel
                    {
                        Id = 2,
                        Codigo = "B1",
                        Fila = 1,
                        Columna = 2,
                        AlmacenId = 1,
                        EstadoEntidad = EstadoEntidad.Activo
                    }
                };

            _anaquelRepositoryMock.Setup(repo => repo.GetAllAnaquelesAsync()).ReturnsAsync(anaqueles);

            // Act
            var result = await _anaquelService.GetAllAnaquelesAsync();

            // Assert
            Assert.Equal(anaqueles, result);
        }

        [Fact]
        public async Task UpdateAnaquelAsync_ValidData_UpdatesAnaquel()
        {
            // Arrange
            var anaquel = new Anaquel
            {
                Id = 1,
                Codigo = "A1-Updated",
                Fila = 1,
                Columna = 1,
                AlmacenId = 1,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _anaquelRepositoryMock.Setup(repo => repo.UpdateAnaquelAsync(It.IsAny<Anaquel>())).Returns(Task.CompletedTask);

            // Act
            await _anaquelService.UpdateAnaquelAsync(anaquel);

            // Assert
            _anaquelRepositoryMock.Verify(repo => repo.UpdateAnaquelAsync(It.IsAny<Anaquel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAnaquelAsync_ValidId_DeletesAnaquel()
        {
            // Arrange
            var anaquelId = 1;

            _anaquelRepositoryMock.Setup(repo => repo.DeleteAnaquelAsync(anaquelId)).Returns(Task.CompletedTask);

            // Act
            await _anaquelService.DeleteAnaquelAsync(anaquelId);

            // Assert
            _anaquelRepositoryMock.Verify(repo => repo.DeleteAnaquelAsync(anaquelId), Times.Once);
        }
    }
}

