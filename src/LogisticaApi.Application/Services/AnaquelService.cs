using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;

namespace RetailProductMicroservice.Application.Services
{
    public class AnaquelService : IAnaquelService
    {
        private readonly IAnaquelRepository _anaquelRepository;

        public AnaquelService(IAnaquelRepository anaquelRepository)
        {
            _anaquelRepository = anaquelRepository;
        }

        public async Task<IEnumerable<Anaquel>> GetAllAnaquelesAsync()
        {
            return await _anaquelRepository.GetAllAnaquelesAsync();
        }

        public async Task<Anaquel?> GetAnaquelByIdAsync(int id)
        {
            return await _anaquelRepository.GetAnaquelByIdAsync(id);
        }

        public async Task<IEnumerable<Anaquel>> GetAnaquelByAlmacenIdAsync(int almacenId)
        {
            return await _anaquelRepository.GetAnaquelByAlmacenIdAsync(almacenId);
        }

        public async Task AddAnaquelAsync(Anaquel anaquel)
        {
            await _anaquelRepository.AddAnaquelAsync(anaquel);
        }

        public async Task UpdateAnaquelAsync(Anaquel anaquel)
        {
            await _anaquelRepository.UpdateAnaquelAsync(anaquel);
        }

        public async Task DeleteAnaquelAsync(int id)
        {
            await _anaquelRepository.DeleteAnaquelAsync(id);
        }
    }
}
