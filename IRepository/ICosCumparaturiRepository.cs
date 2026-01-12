using mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.IRepository
{
    public interface ICosCumparaturiRepository
    {
        Task<IEnumerable<Cos_cumparaturi>> GetAllAsync();
        Task<Cos_cumparaturi?> GetByIdAsync(int id);
        Task AddAsync(Cos_cumparaturi cos);
        Task UpdateAsync(Cos_cumparaturi cos);
        Task DeleteAsync(int id);
        Task<IEnumerable<Cos_cumparaturi>> GetByUserIdAsync(string userId); // Add this line
    }
}
