using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Application.Interfaces;

public interface IMovimientoService
{
    Task<IEnumerable<Movimiento>> GetAllMovimientosAsync();

    Task<Movimiento?> GetMovimientoByIdAsync(int id);

    Task AddMovimientoAsync(Movimiento movimiento);

    Task UpdateMovimientoAsync(Movimiento movimiento);

    Task DeleteMovimientoAsync(int id);

    Task<IEnumerable<Movimiento>> GetMovimientosByDateRangeAsync(DateTime startDate, DateTime endDate);

    Task<IEnumerable<Movimiento>> GetMovimientosByAlmacenAsync(int almacenId);

    Task<IEnumerable<Movimiento>> GetMovimientosByAnaquelAsync(int anaquelId);

    Task<IEnumerable<Movimiento>> GetMovimientosByMotivoAsync(MotivoMovimiento motivo);

    Task<IEnumerable<Movimiento>> GetMovimientosByProductoAsync(int productoId);

    Task<IEnumerable<Movimiento>> GetMovimientosByCantidadAsync(decimal cantidad);
}
