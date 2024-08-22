using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace RetailProductMicroservice.Tests.IntegrationTests
{
    public class LogisticControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public LogisticControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterIncomingMovement_ReturnsCreated()
        {
            // Arrange
            var movement = new
            {
                ProductId = 1,
                MovementType = "Incoming",
                Quantity = 10,
                MovementDate = DateTime.Now,
                Reason = "New stock",
                SourceWarehouseId = 1,
                DestinationWarehouseId = 2
            };

            var content = new StringContent(JsonConvert.SerializeObject(movement), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/logistic/incoming", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task RegisterOutgoingMovement_ReturnsCreated()
        {
            // Arrange
            var movement = new
            {
                ProductId = 1,
                MovementType = "Outgoing",
                Quantity = 5,
                MovementDate = DateTime.Now,
                Reason = "Sale",
                SourceWarehouseId = 1,
                DestinationWarehouseId = 2
            };

            var content = new StringContent(JsonConvert.SerializeObject(movement), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/logistic/outgoing", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task RegisterTransferMovement_ReturnsCreated()
        {
            // Arrange
            var movement = new
            {
                ProductId = 1,
                MovementType = "Transfer",
                Quantity = 3,
                MovementDate = DateTime.Now,
                Reason = "Warehouse consolidation",
                SourceWarehouseId = 1,
                DestinationWarehouseId = 2
            };

            var content = new StringContent(JsonConvert.SerializeObject(movement), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/logistic/transfer", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}