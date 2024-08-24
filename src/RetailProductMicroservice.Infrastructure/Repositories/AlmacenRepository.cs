using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Infrastructure.Repositories;

public class AlmacenRepository : IAlmacenRepository
{
    private readonly RetailProductContext _context;

    public AlmacenRepository(RetailProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Almacen>> GetAllAlmacenesAsync()
    {
        return await _context.Almacenes
            .Where(a => a.EstadoEntidad == EstadoEntidad.Activo)
            .ToListAsync();
    }

    public async Task<Almacen?> GetAlmacenByIdAsync(int id)
    {
        return await _context.Almacenes
            .FirstOrDefaultAsync(a => a.EstadoEntidad == EstadoEntidad.Activo && a.Id == id);
    }

    public async Task AddAlmacenAsync(Almacen almacen)
    {
        _context.Almacenes.Add(almacen);
        almacen.EstadoEntidad = EstadoEntidad.Activo;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAlmacenAsync(Almacen almacen)
    {
        _context.Entry(almacen).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAlmacenAsync(int id)
    {
        var almacen = await _context.Almacenes.FindAsync(id);
        if (almacen != null)
        {
            almacen.EstadoEntidad = EstadoEntidad.Inactivo;
            await _context.SaveChangesAsync();
        }
    }
}
