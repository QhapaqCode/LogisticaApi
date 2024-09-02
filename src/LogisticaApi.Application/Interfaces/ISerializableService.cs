using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Application.Interfaces;

public interface ISerializableService
{
    Task<IEnumerable<Serializable>> GetAllSerializablesAsync();

    Task<Serializable> GetSerializableByIdAsync(int id);

    Task AddSerializableAsync(Serializable serializable);

    Task UpdateSerializableAsync(Serializable serializable);

    Task DeleteSerializableAsync(int id);
}
