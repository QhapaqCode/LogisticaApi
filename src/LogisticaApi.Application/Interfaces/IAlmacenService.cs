using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Application.Interfaces;

public interface IAlmacenService
{
    Task<IEnumerable<Almacen>> GetAllAlmacenesAsync();

    Task<Almacen?> GetAlmacenByIdAsync(int id);

    Task AddAlmacenAsync(Almacen almacen);

    Task UpdateAlmacenAsync(Almacen almacen);

    Task DeleteAlmacenAsync(int id);
}
