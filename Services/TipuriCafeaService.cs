using mvc.Models;
using mvc.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using mvc.Repository;

namespace mvc.Services
{
    public class TipuriCafeaService : ITipuriCafeaService
    {
        private readonly ITipuriCafeaRepository _tipuriCafeaRepository;

        public TipuriCafeaService(ITipuriCafeaRepository tipuriCafeaRepository)
        {
            _tipuriCafeaRepository = tipuriCafeaRepository;
        }

        public Task<IEnumerable<Tipuri_Cafea>> GetAllAsync() => _tipuriCafeaRepository.GetAllAsync();
        public Task<Tipuri_Cafea?> GetByIdAsync(int id) => _tipuriCafeaRepository.GetByIdAsync(id);
        public Task AddAsync(Tipuri_Cafea cafea) => _tipuriCafeaRepository.AddAsync(cafea);
        public Task UpdateAsync(Tipuri_Cafea cafea) => _tipuriCafeaRepository.UpdateAsync(cafea);
        public Task DeleteAsync(int id) => _tipuriCafeaRepository.DeleteAsync(id);
    }
}
