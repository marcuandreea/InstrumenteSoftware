using mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Services
{
    public interface IEspressoareService
    {
        Task<IEnumerable<Espressoare>> GetAllAsync();
        Task<Espressoare?> GetByIdAsync(int id);
        Task AddAsync(Espressoare espressoare);
        Task UpdateAsync(Espressoare espressoare);
        Task DeleteAsync(int id);
    }
}
