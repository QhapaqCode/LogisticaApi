using Moq;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Application.Services;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using Xunit;

namespace RetailProductMicroservice.Tests.UnitTests
{
    public class SerializableServiceTests
    {
        private readonly Mock<ISerializableRepository> _serializableRepositoryMock;
        private readonly ISerializableService _serializableService;

        public SerializableServiceTests()
        {
            _serializableRepositoryMock = new Mock<ISerializableRepository>();
            _serializableService = new SerializableService(_serializableRepositoryMock.Object);
        }

        [Fact]
        public async Task AddSerializableAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var serializable = new Serializable
            {
                Id = 1,
                Nombre = "Producto A",
                Descripcion = "Descripción del Producto A",
                EstadoEntidad = EstadoEntidad.Activo,
                Serie = "12345",
                EstadoProducto = EstadoProducto.Nuevo
            };

            _serializableRepositoryMock.Setup(repo => repo.AddSerializableAsync(It.IsAny<Serializable>())).Returns(Task.CompletedTask);

            // Act
            await _serializableService.AddSerializableAsync(serializable);

            // Assert
            _serializableRepositoryMock.Verify(repo => repo.AddSerializableAsync(It.IsAny<Serializable>()), Times.Once);
        }

        [Fact]
        public async Task GetSerializableByIdAsync_ValidId_ReturnsSerializable()
        {
            // Arrange
            var serializableId = 1;
            var serializable = new Serializable
            {
                Id = serializableId,
                Nombre = "Producto A",
                Descripcion = "Descripción del Producto A",
                EstadoEntidad = EstadoEntidad.Activo,
                Serie = "12345",
                EstadoProducto = EstadoProducto.Nuevo
            };

            _serializableRepositoryMock.Setup(repo => repo.GetSerializableByIdAsync(serializableId)).ReturnsAsync(serializable);

            // Act
            var result = await _serializableService.GetSerializableByIdAsync(serializableId);

            // Assert
            Assert.Equal(serializable, result);
        }

        [Fact]
        public async Task GetAllSerializablesAsync_ReturnsSerializables()
        {
            // Arrange
            var serializables = new List<Serializable>
                {
                    new Serializable
                    {
                        Id = 1,
                        Nombre = "Producto A",
                        Descripcion = "Descripción del Producto A",
                        EstadoEntidad = EstadoEntidad.Activo,
                        Serie = "12345",
                        EstadoProducto = EstadoProducto.Nuevo
                    },
                    new Serializable
                    {
                        Id = 2,
                        Nombre = "Producto B",
                        Descripcion = "Descripción del Producto B",
                        EstadoEntidad = EstadoEntidad.Activo,
                        Serie = "67890",
                        EstadoProducto = EstadoProducto.Usado
                    }
                };

            _serializableRepositoryMock.Setup(repo => repo.GetAllSerializablesAsync()).ReturnsAsync(serializables);

            // Act
            var result = await _serializableService.GetAllSerializablesAsync();

            // Assert
            Assert.Equal(serializables, result);
        }

        [Fact]
        public async Task UpdateSerializableAsync_ValidData_UpdatesSerializable()
        {
            // Arrange
            var serializable = new Serializable
            {
                Id = 1,
                Nombre = "Producto A Actualizado",
                Descripcion = "Descripción del Producto A Actualizado",
                EstadoEntidad = EstadoEntidad.Activo,
                Serie = "12345",
                EstadoProducto = EstadoProducto.Nuevo
            };

            _serializableRepositoryMock.Setup(repo => repo.UpdateSerializableAsync(It.IsAny<Serializable>())).Returns(Task.CompletedTask);

            // Act
            await _serializableService.UpdateSerializableAsync(serializable);

            // Assert
            _serializableRepositoryMock.Verify(repo => repo.UpdateSerializableAsync(It.IsAny<Serializable>()), Times.Once);
        }

        [Fact]
        public async Task DeleteSerializableAsync_ValidId_DeletesSerializable()
        {
            // Arrange
            var serializableId = 1;

            _serializableRepositoryMock.Setup(repo => repo.DeleteSerializableAsync(serializableId)).Returns(Task.CompletedTask);

            // Act
            await _serializableService.DeleteSerializableAsync(serializableId);

            // Assert
            _serializableRepositoryMock.Verify(repo => repo.DeleteSerializableAsync(serializableId), Times.Once);
        }
    }
}
