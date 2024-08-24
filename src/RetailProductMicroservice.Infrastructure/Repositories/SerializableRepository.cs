using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Infrastructure.Repositories;

public class SerializableRepository : ISerializableRepository
{
    private readonly RetailProductContext _context;

    public SerializableRepository(RetailProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Serializable>> GetAllSerializablesAsync()
    {
        return await _context.Serializables.ToListAsync();
    }

    public async Task<Serializable?> GetSerializableByIdAsync(int id)
    {
        return await _context.Serializables.FindAsync(id);
    }

    public async Task AddSerializableAsync(Serializable serializable)
    {
        _context.Serializables.Add(serializable);
        serializable.EstadoEntidad = EstadoEntidad.Activo;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSerializableAsync(Serializable serializable)
    {
        _context.Entry(serializable).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSerializableAsync(int id)
    {
        var serializable = await _context.Serializables.FindAsync(id);
        if (serializable != null)
        {
            serializable.EstadoEntidad = EstadoEntidad.Inactivo;
            await _context.SaveChangesAsync();
        }
    }
}
