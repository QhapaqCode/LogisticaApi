using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Infrastructure.Repositories;

public class MovimientoRepository : IMovimientoRepository
{
    private readonly RetailProductContext _context;

    public MovimientoRepository(RetailProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movimiento>> GetAllMovimientosAsync()
    {
        return await _context.Movimientos
            .Where(m => m.EstadoEntidad == EstadoEntidad.Activo)
            .ToListAsync();
    }

    public async Task<Movimiento?> GetMovimientoByIdAsync(int id)
    {
        return await _context.Movimientos
            .FirstOrDefaultAsync(m => m.EstadoEntidad == EstadoEntidad.Activo && m.Id == id);
    }

    public async Task AddMovimientoAsync(Movimiento movimiento)
    {
        movimiento.EstadoEntidad = EstadoEntidad.Activo;
        _context.Movimientos.Add(movimiento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMovimientoAsync(Movimiento movimiento)
    {
        _context.Entry(movimiento).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMovimientoAsync(int id)
    {
        var movimiento = await _context.Movimientos.FindAsync(id);
        if (movimiento != null)
        {
            movimiento.EstadoEntidad = EstadoEntidad.Inactivo;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Movimiento>> GetMovimientosByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Movimientos
            .Where(m => m.EstadoEntidad == EstadoEntidad.Activo && m.Fecha >= startDate && m.Fecha <= endDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movimiento>> GetMovimientosByAlmacenAsync(int almacenId)
    {
        return await _context.Movimientos
            .Where(m => m.EstadoEntidad == EstadoEntidad.Activo && m.AlmacenId == almacenId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movimiento>> GetMovimientosByAnaquelAsync(int anaquelId)
    {
        return await _context.Movimientos
            .Where(m => m.EstadoEntidad == EstadoEntidad.Activo && m.AnaquelId == anaquelId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movimiento>> GetMovimientosByMotivoAsync(MotivoMovimiento motivo)
    {
        return await _context.Movimientos
            .Where(m => m.EstadoEntidad == EstadoEntidad.Activo && m.Motivo == motivo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movimiento>> GetMovimientosByProductoAsync(int productoId)
    {
        return await _context.Movimientos
            .Where(m => m.EstadoEntidad == EstadoEntidad.Activo && m.ProductoId == productoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movimiento>> GetMovimientosByCantidadAsync(decimal cantidad)
    {
        return await _context.Movimientos
            .Where(m => m.EstadoEntidad == EstadoEntidad.Activo && m.Cantidad == cantidad)
            .ToListAsync();
    }
}
