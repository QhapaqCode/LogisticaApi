using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Domain.Interfaces
{
    public interface ILogisticRepository
    {
        Task<IEnumerable<Logistic>> GetAllLogistics();
        Task<Logistic> GetLogisticById(Guid id);
        Task<IEnumerable<Logistic>> GetLogisticsByProductId(Guid productId);
        Task<IEnumerable<Logistic>> GetLogisticsByWarehouseId(Guid warehouseId);
        Task<IEnumerable<Logistic>> GetIncomingLogistics();
        Task<IEnumerable<Logistic>> GetOutgoingLogistics();
        Task<IEnumerable<Logistic>> GetTransferLogistics();
        Task AddLogistic(Logistic logistic);
        Task UpdateLogistic(Logistic logistic);
        Task DeleteLogistic(Guid id);
    }
}