using RetailProductMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RetailProductMicroservice.Domain.Interfaces;

public interface IAnaquelRepository
{
    Task<IEnumerable<Anaquel>> GetAllAnaquelesAsync();

    Task<Anaquel?> GetAnaquelByIdAsync(int id);

    Task<IEnumerable<Anaquel>> GetAnaquelByAlmacenIdAsync(int almacenId);

    Task AddAnaquelAsync(Anaquel anaquel);

    Task UpdateAnaquelAsync(Anaquel anaquel);

    Task DeleteAnaquelAsync(int id);
}
