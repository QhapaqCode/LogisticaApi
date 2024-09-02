using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Application.Interfaces;

public interface IExistenciaService
{
    Task<IEnumerable<Existencia>> GetAllExistenciasAsync();

    Task<Existencia> GetExistenciaByIdAsync(int id);

    Task AddExistenciaAsync(Existencia existencia);

    Task UpdateExistenciaAsync(Existencia existencia);

    Task DeleteExistenciaAsync(int id);
}
