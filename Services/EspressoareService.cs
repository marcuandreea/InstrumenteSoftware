using mvc.Models;
using mvc.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Services
{
    public class EspressoareService : IEspressoareService
    {
        private readonly IEspressoareRepository _espressoareRepository;

        public EspressoareService(IEspressoareRepository espressoareRepository)
        {
            _espressoareRepository = espressoareRepository;
        }

        public Task<IEnumerable<Espressoare>> GetAllAsync() => _espressoareRepository.GetAllAsync();
        public Task<Espressoare?> GetByIdAsync(int id) => _espressoareRepository.GetByIdAsync(id);
        public Task AddAsync(Espressoare espressoare) => _espressoareRepository.AddAsync(espressoare);
        public Task UpdateAsync(Espressoare espressoare) => _espressoareRepository.UpdateAsync(espressoare);
        public Task DeleteAsync(int id) => _espressoareRepository.DeleteAsync(id);
    }
}
