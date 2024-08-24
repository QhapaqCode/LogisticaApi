using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;
using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Application.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientoService(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        public async Task<IEnumerable<Movimiento>> GetAllMovimientosAsync()
        {
            return await _movimientoRepository.GetAllMovimientosAsync();
        }

        public async Task<Movimiento?> GetMovimientoByIdAsync(int id)
        {
            return await _movimientoRepository.GetMovimientoByIdAsync(id);
        }

        public async Task AddMovimientoAsync(Movimiento movimiento)
        {
            await _movimientoRepository.AddMovimientoAsync(movimiento);
        }

        public async Task UpdateMovimientoAsync(Movimiento movimiento)
        {
            await _movimientoRepository.UpdateMovimientoAsync(movimiento);
        }

        public async Task DeleteMovimientoAsync(int id)
        {
            await _movimientoRepository.DeleteMovimientoAsync(id);
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _movimientoRepository.GetMovimientosByDateRangeAsync(startDate, endDate);
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByAlmacenAsync(int almacenId)
        {
            return await _movimientoRepository.GetMovimientosByAlmacenAsync(almacenId);
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByAnaquelAsync(int anaquelId)
        {
            return await _movimientoRepository.GetMovimientosByAnaquelAsync(anaquelId);
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByMotivoAsync(MotivoMovimiento motivo)
        {
            return await _movimientoRepository.GetMovimientosByMotivoAsync(motivo);
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByProductoAsync(int productoId)
        {
            return await _movimientoRepository.GetMovimientosByProductoAsync(productoId);
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByCantidadAsync(decimal cantidad)
        {
            return await _movimientoRepository.GetMovimientosByCantidadAsync(cantidad);
        }
    }
}
