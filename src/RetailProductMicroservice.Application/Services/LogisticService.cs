using System;
using System.Threading.Tasks;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;

namespace RetailProductMicroservice.Application.Services
{
    public class LogisticService : ILogisticService
    {
        private readonly ILogisticRepository _logisticRepository;

        public LogisticService(ILogisticRepository logisticRepository)
        {
            _logisticRepository = logisticRepository;
        }

        public async Task RegisterIncomingMovement(Product product, int quantity, string reason, int sourceWarehouseId)
        {
            var logistic = new Logistic
            {
                ProductId = product.Id,
                MovementType = MovementType.Incoming,
                Quantity = quantity,
                MovementDate = DateTime.Now,
                Reason = reason,
                SourceWarehouseId = sourceWarehouseId
            };

            await _logisticRepository.AddLogistic(logistic);
        }

        public async Task RegisterOutgoingMovement(Product product, int quantity, string reason, int destinationWarehouseId)
        {
            var logistic = new Logistic
            {
                ProductId = product.Id,
                MovementType = MovementType.Outgoing,
                Quantity = quantity,
                MovementDate = DateTime.Now,
                Reason = reason,
                DestinationWarehouseId = destinationWarehouseId
            };

            await _logisticRepository.AddLogistic(logistic);
        }

        public async Task RegisterTransferMovement(Product product, int quantity, string reason, int sourceWarehouseId, int destinationWarehouseId)
        {
            var logistic = new Logistic
            {
                ProductId = product.Id,
                MovementType = MovementType.Transfer,
                Quantity = quantity,
                MovementDate = DateTime.Now,
                Reason = reason,
                SourceWarehouseId = sourceWarehouseId,
                DestinationWarehouseId = destinationWarehouseId
            };

            await _logisticRepository.AddLogistic(logistic);
        }
    }
}