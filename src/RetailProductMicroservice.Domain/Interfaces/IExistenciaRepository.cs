using RetailProductMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RetailProductMicroservice.Domain.Interfaces;

public interface IExistenciaRepository
{
    Task<IEnumerable<Existencia>> GetAllExistenciasAsync();

    Task<Existencia> GetExistenciaByIdAsync(int id);

    Task AddExistenciaAsync(Existencia existencia);

    Task UpdateExistenciaAsync(Existencia existencia);

    Task DeleteExistenciaAsync(int id);
}
