using RetailProductMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RetailProductMicroservice.Domain.Interfaces;

public interface ISerializableRepository
{
    Task<IEnumerable<Serializable>> GetAllSerializablesAsync();

    Task<Serializable> GetSerializableByIdAsync(int id);

    Task AddSerializableAsync(Serializable serializable);

    Task UpdateSerializableAsync(Serializable serializable);

    Task DeleteSerializableAsync(int id);
}
