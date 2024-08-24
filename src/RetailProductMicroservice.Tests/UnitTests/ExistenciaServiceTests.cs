using Moq;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Application.Services;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using Xunit;

namespace RetailProductMicroservice.Tests.UnitTests
{
    public class ExistenciaServiceTests
    {
        private readonly Mock<IExistenciaRepository> _existenciaRepositoryMock;
        private readonly IExistenciaService _existenciaService;

        public ExistenciaServiceTests()
        {
            _existenciaRepositoryMock = new Mock<IExistenciaRepository>();
            _existenciaService = new ExistenciaService(_existenciaRepositoryMock.Object);
        }

        [Fact]
        public async Task AddExistenciaAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var existencia = new Existencia
            {
                Id = 1,
                Nombre = "Producto Test",
                Descripcion = "Descripción Test",
                TipoProducto = TipoProducto.Accesorio,
                Codigo = "EX123",
                UnidadMedida = UnidadMedida.Kilogramo,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _existenciaRepositoryMock.Setup(repo => repo.AddExistenciaAsync(It.IsAny<Existencia>())).Returns(Task.CompletedTask);

            // Act
            await _existenciaService.AddExistenciaAsync(existencia);

            // Assert
            _existenciaRepositoryMock.Verify(repo => repo.AddExistenciaAsync(It.IsAny<Existencia>()), Times.Once);
        }

        [Fact]
        public async Task GetExistenciaByIdAsync_ValidId_ReturnsExistencia()
        {
            // Arrange
            var existenciaId = 1;
            var existencia = new Existencia
            {
                Id = existenciaId,
                Nombre = "Producto Test",
                Descripcion = "Descripción Test",
                TipoProducto = TipoProducto.Individual,
                Codigo = "EX123",
                UnidadMedida = UnidadMedida.Kilogramo,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _existenciaRepositoryMock.Setup(repo => repo.GetExistenciaByIdAsync(existenciaId)).ReturnsAsync(existencia);

            // Act
            var result = await _existenciaService.GetExistenciaByIdAsync(existenciaId);

            // Assert
            Assert.Equal(existencia, result);
        }

        [Fact]
        public async Task GetAllExistenciasAsync_ReturnsExistencias()
        {
            // Arrange
            var existencias = new List<Existencia>
                {
                    new Existencia
                    {
                        Id = 1,
                        Nombre = "Producto Test 1",
                        Descripcion = "Descripción Test 1",
                        TipoProducto = TipoProducto.Accesorio,
                        Codigo = "EX123",
                        UnidadMedida = UnidadMedida.Kilogramo,
                        EstadoEntidad = EstadoEntidad.Activo
                    },
                    new Existencia
                    {
                        Id = 2,
                        Nombre = "Producto Test 2",
                        Descripcion = "Descripción Test 2",
                        TipoProducto = TipoProducto.Individual,
                        Codigo = "EX124",
                        UnidadMedida = UnidadMedida.Litro,
                        EstadoEntidad = EstadoEntidad.Activo
                    }
                };

            _existenciaRepositoryMock.Setup(repo => repo.GetAllExistenciasAsync()).ReturnsAsync(existencias);

            // Act
            var result = await _existenciaService.GetAllExistenciasAsync();

            // Assert
            Assert.Equal(existencias, result);
        }

        [Fact]
        public async Task UpdateExistenciaAsync_ValidData_UpdatesExistencia()
        {
            // Arrange
            var existencia = new Existencia
            {
                Id = 1,
                Nombre = "Producto Test Actualizado",
                Descripcion = "Descripción Test Actualizada",
                TipoProducto = TipoProducto.Perecible,
                Codigo = "EX123",
                UnidadMedida = UnidadMedida.Kilogramo,
                EstadoEntidad = EstadoEntidad.Activo
            };

            _existenciaRepositoryMock.Setup(repo => repo.UpdateExistenciaAsync(It.IsAny<Existencia>())).Returns(Task.CompletedTask);

            // Act
            await _existenciaService.UpdateExistenciaAsync(existencia);

            // Assert
            _existenciaRepositoryMock.Verify(repo => repo.UpdateExistenciaAsync(It.IsAny<Existencia>()), Times.Once);
        }

        [Fact]
        public async Task DeleteExistenciaAsync_ValidId_DeletesExistencia()
        {
            // Arrange
            var existenciaId = 1;

            _existenciaRepositoryMock.Setup(repo => repo.DeleteExistenciaAsync(existenciaId)).Returns(Task.CompletedTask);

            // Act
            await _existenciaService.DeleteExistenciaAsync(existenciaId);

            // Assert
            _existenciaRepositoryMock.Verify(repo => repo.DeleteExistenciaAsync(existenciaId), Times.Once);
        }
    }
}

