using mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Services
{
    public interface ITipuriCafeaService
    {
        Task<IEnumerable<Tipuri_Cafea>> GetAllAsync();
        Task<Tipuri_Cafea?> GetByIdAsync(int id);
        Task AddAsync(Tipuri_Cafea cafea);
        Task UpdateAsync(Tipuri_Cafea cafea);
        Task DeleteAsync(int id);
    }
}
