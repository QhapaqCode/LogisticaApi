using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Application.Interfaces
{
    public interface ILogisticService
    {
        Task<IEnumerable<Logistic>> GetIncomingProductMovementsAsync();
        Task<IEnumerable<Logistic>> GetOutgoingProductMovementsAsync();
        Task<IEnumerable<Logistic>> GetWarehouseTransfersAsync();
        Task<Logistic> GetLogisticMovementByIdAsync(Guid id);
        Task RegisterIncomingProductMovementAsync(Logistic logistic);
        Task RegisterOutgoingProductMovementAsync(Logistic logistic);
        Task RegisterWarehouseTransferAsync(Logistic logistic);
        Task UpdateLogisticMovementAsync(Logistic logistic);
        Task DeleteLogisticMovementAsync(Guid id);
    }
}