using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Tests.UnitTests
{
    public class LogisticServiceTests
    {
        private readonly Mock<ILogisticRepository> _logisticRepositoryMock;
        private readonly ILogisticService _logisticService;

        public LogisticServiceTests()
        {
            _logisticRepositoryMock = new Mock<ILogisticRepository>();
            _logisticService = new LogisticService(_logisticRepositoryMock.Object);
        }

        [Fact]
        public async Task RegisterIncomingMovement_ValidData_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var quantity = 10;
            var movementDate = DateTime.Now;
            var reason = "New stock";
            var sourceWarehouseId = Guid.NewGuid();
            var destinationWarehouseId = Guid.NewGuid();

            var logistic = new Logistic
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                MovementType = MovementType.Incoming,
                Quantity = quantity,
                MovementDate = movementDate,
                Reason = reason,
                SourceWarehouseId = sourceWarehouseId,
                DestinationWarehouseId = destinationWarehouseId
            };

            _logisticRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Logistic>())).ReturnsAsync(logistic);

            // Act
            var result = await _logisticService.RegisterIncomingMovement(productId, quantity, movementDate, reason, sourceWarehouseId, destinationWarehouseId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetProductMovements_ValidProductId_ReturnsProductMovements()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var movements = new List<Logistic>
            {
                new Logistic
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    MovementType = MovementType.Incoming,
                    Quantity = 10,
                    MovementDate = DateTime.Now.AddDays(-1),
                    Reason = "New stock",
                    SourceWarehouseId = Guid.NewGuid(),
                    DestinationWarehouseId = Guid.NewGuid()
                },
                new Logistic
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    MovementType = MovementType.Outgoing,
                    Quantity = 5,
                    MovementDate = DateTime.Now,
                    Reason = "Sales",
                    SourceWarehouseId = Guid.NewGuid(),
                    DestinationWarehouseId = Guid.NewGuid()
                }
            };

            _logisticRepositoryMock.Setup(repo => repo.GetProductMovementsAsync(productId)).ReturnsAsync(movements);

            // Act
            var result = await _logisticService.GetProductMovements(productId);

            // Assert
            Assert.Equal(movements, result);
        }
    }
}