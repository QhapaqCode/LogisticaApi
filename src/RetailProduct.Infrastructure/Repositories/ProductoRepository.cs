using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly RetailProductContext _context;

    public ProductoRepository(RetailProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAllProductsAsync()
    {
        return await _context.Productos
            .Where(p => p.EstadoEntidad == EstadoEntidad.Activo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Producto>> GetProductsByAlmacenIdAsync(int almacenId)
    {
        return await _context.Movimientos
            .Where(m => m.AlmacenId == almacenId)
            .Select(m => m.Producto)
            .Where(p => p.EstadoEntidad == EstadoEntidad.Activo)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<Producto>> GetProductsByAnaquelIdAsync(int anaquelId)
    {
        return await _context.Movimientos
            .Where(m => m.AnaquelId == anaquelId)
            .Select(m => m.Producto)
            .Where(p => p.EstadoEntidad == EstadoEntidad.Activo)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Producto?> GetProductByIdAsync(int id)
    {
        return await _context.Productos.FindAsync(id);
    }

    public async Task AddProductAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        producto.EstadoEntidad = EstadoEntidad.Activo;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Producto producto)
    {
        _context.Entry(producto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            producto.EstadoEntidad = EstadoEntidad.Inactivo;
            await _context.SaveChangesAsync();
        }
    }
}
