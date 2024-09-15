using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Infrastructure.Repositories;

public class ExistenciaRepository : IExistenciaRepository
{
    private readonly RetailProductContext _context;

    public ExistenciaRepository(RetailProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Existencia>> GetAllExistenciasAsync()
    {
        return await _context.Existencias.ToListAsync();
    }

    public async Task<Existencia?> GetExistenciaByIdAsync(int id)
    {
        return await _context.Existencias.FindAsync(id);
    }

    public async Task AddExistenciaAsync(Existencia existencia)
    {
        _context.Existencias.Add(existencia);
        existencia.EstadoEntidad = EstadoEntidad.Activo;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateExistenciaAsync(Existencia existencia)
    {
        _context.Entry(existencia).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExistenciaAsync(int id)
    {
        var existencia = await _context.Existencias.FindAsync(id);
        if (existencia != null)
        {
            existencia.EstadoEntidad = EstadoEntidad.Inactivo;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Clear()
    {
        _context.Existencias.RemoveRange(_context.Existencias);
        await _context.SaveChangesAsync();
    }
}
