using mvc.Models;
using mvc.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Services
{
    public class CosCumparaturiService : ICosCumparaturiService
    {
        private readonly ICosCumparaturiRepository _cosCumparaturiRepository;

        public CosCumparaturiService(ICosCumparaturiRepository cosCumparaturiRepository)
        {
            _cosCumparaturiRepository = cosCumparaturiRepository;
        }

        public Task<IEnumerable<Cos_cumparaturi>> GetAllAsync() => _cosCumparaturiRepository.GetAllAsync();
        public Task<Cos_cumparaturi?> GetByIdAsync(int id) => _cosCumparaturiRepository.GetByIdAsync(id);
        public Task AddAsync(Cos_cumparaturi cos) => _cosCumparaturiRepository.AddAsync(cos);
        public Task UpdateAsync(Cos_cumparaturi cos) => _cosCumparaturiRepository.UpdateAsync(cos);
        public Task DeleteAsync(int id) => _cosCumparaturiRepository.DeleteAsync(id);
        public Task<IEnumerable<Cos_cumparaturi>> GetByUserIdAsync(string userId)
    => _cosCumparaturiRepository.GetByUserIdAsync(userId);

    }
}
