using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Infrastructure.Repositories;

public class AnaquelRepository : IAnaquelRepository
{
    private readonly RetailProductContext _context;

    public AnaquelRepository(RetailProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Anaquel>> GetAllAnaquelesAsync()
    {
        return await _context.Anaqueles.Where(a => a.EstadoEntidad == EstadoEntidad.Activo).ToListAsync();
    }

    public async Task<Anaquel?> GetAnaquelByIdAsync(int id)
    {
        return await _context.Anaqueles.Where(a => a.EstadoEntidad == EstadoEntidad.Activo && a.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAnaquelAsync(Anaquel anaquel)
    {
        _context.Anaqueles.Add(anaquel);
        anaquel.EstadoEntidad = EstadoEntidad.Activo;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAnaquelAsync(Anaquel anaquel)
    {
        _context.Entry(anaquel).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAnaquelAsync(int id)
    {
        var anaquel = await _context.Anaqueles.FindAsync(id);
        if (anaquel != null)
        {
            anaquel.EstadoEntidad = EstadoEntidad.Inactivo;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Anaquel>> GetAnaquelByAlmacenIdAsync(int almacenId)
    {
        return await _context.Anaqueles.Where(a => a.EstadoEntidad == EstadoEntidad.Activo && a.AlmacenId == almacenId).ToListAsync();
    }
}
