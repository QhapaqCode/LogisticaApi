using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;

namespace RetailProductMicroservice.Application.Services
{
    public class SerializableService : ISerializableService
    {
        private readonly ISerializableRepository _serializableRepository;

        public SerializableService(ISerializableRepository serializableRepository)
        {
            _serializableRepository = serializableRepository;
        }

        public async Task<IEnumerable<Serializable>> GetAllSerializablesAsync()
        {
            return await _serializableRepository.GetAllSerializablesAsync();
        }

        public async Task<Serializable> GetSerializableByIdAsync(int id)
        {
            return await _serializableRepository.GetSerializableByIdAsync(id);
        }

        public async Task AddSerializableAsync(Serializable serializable)
        {
            await _serializableRepository.AddSerializableAsync(serializable);
        }

        public async Task UpdateSerializableAsync(Serializable serializable)
        {
            await _serializableRepository.UpdateSerializableAsync(serializable);
        }

        public async Task DeleteSerializableAsync(int id)
        {
            await _serializableRepository.DeleteSerializableAsync(id);
        }
    }
}
