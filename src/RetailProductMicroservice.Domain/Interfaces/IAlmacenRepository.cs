using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Domain.Interfaces;

public interface IAlmacenRepository
{
    Task<IEnumerable<Almacen>> GetAllAlmacenesAsync();

    Task<Almacen?> GetAlmacenByIdAsync(int id);

    Task AddAlmacenAsync(Almacen almacen);

    Task UpdateAlmacenAsync(Almacen almacen);

    Task DeleteAlmacenAsync(int id);
}
