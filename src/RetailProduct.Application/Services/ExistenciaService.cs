using RetailProductMicroservice.Application.Interfaces;
using RetailProductMicroservice.Domain.Entities;
using RetailProductMicroservice.Domain.Interfaces;

namespace RetailProductMicroservice.Application.Services
{
    public class ExistenciaService : IExistenciaService
    {
        private readonly IExistenciaRepository _existenciaRepository;

        public ExistenciaService(IExistenciaRepository existenciaRepository)
        {
            _existenciaRepository = existenciaRepository;
        }

        public async Task<IEnumerable<Existencia>> GetAllExistenciasAsync()
        {
            return await _existenciaRepository.GetAllExistenciasAsync();
        }

        public async Task<Existencia> GetExistenciaByIdAsync(int id)
        {
            return await _existenciaRepository.GetExistenciaByIdAsync(id);
        }

        public async Task AddExistenciaAsync(Existencia existencia)
        {
            await _existenciaRepository.AddExistenciaAsync(existencia);
        }

        public async Task UpdateExistenciaAsync(Existencia existencia)
        {
            await _existenciaRepository.UpdateExistenciaAsync(existencia);
        }

        public async Task DeleteExistenciaAsync(int id)
        {
            await _existenciaRepository.DeleteExistenciaAsync(id);
        }
    }
}
