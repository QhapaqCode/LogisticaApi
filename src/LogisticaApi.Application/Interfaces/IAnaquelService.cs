using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Application.Interfaces;

public interface IAnaquelService
{
    Task<IEnumerable<Anaquel>> GetAllAnaquelesAsync();

    Task<Anaquel?> GetAnaquelByIdAsync(int id);

    Task<IEnumerable<Anaquel>> GetAnaquelByAlmacenIdAsync(int almacenId);

    Task AddAnaquelAsync(Anaquel anaquel);

    Task UpdateAnaquelAsync(Anaquel anaquel);

    Task DeleteAnaquelAsync(int id);
}
