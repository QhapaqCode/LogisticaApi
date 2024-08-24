using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;

namespace RetailProductMicroservice.Application.Services
{
    public class AlmacenService : IAlmacenService
    {
        private readonly IAlmacenRepository _almacenRepository;

        public AlmacenService(IAlmacenRepository almacenRepository)
        {
            _almacenRepository = almacenRepository;
        }

        public async Task<IEnumerable<Almacen>> GetAllAlmacenesAsync()
        {
            return await _almacenRepository.GetAllAlmacenesAsync();
        }

        public async Task<Almacen?> GetAlmacenByIdAsync(int id)
        {
            return await _almacenRepository.GetAlmacenByIdAsync(id);
        }

        public async Task AddAlmacenAsync(Almacen almacen)
        {
            await _almacenRepository.AddAlmacenAsync(almacen);
        }

        public async Task UpdateAlmacenAsync(Almacen almacen)
        {
            await _almacenRepository.UpdateAlmacenAsync(almacen);
        }

        public async Task DeleteAlmacenAsync(int id)
        {
            await _almacenRepository.DeleteAlmacenAsync(id);
        }
    }
}
