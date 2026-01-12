using mvc.Models;
using mvc.IRepository;
using mvc.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Repository
{
    public class EspressoareRepository : IEspressoareRepository
    {
        private readonly AppDbContext _context;

        public EspressoareRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Espressoare>> GetAllAsync()
        {
            return await _context.Espressoare.ToListAsync();
        }

        public async Task<Espressoare?> GetByIdAsync(int id)
        {
            return await _context.Espressoare
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.id == id);
        }


        public async Task AddAsync(Espressoare espressoare)
        {
            _context.Espressoare.Add(espressoare);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Espressoare espressoare)
        {
            _context.Espressoare.Update(espressoare);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var espressoare = await _context.Espressoare.FindAsync(id);
            if (espressoare != null)
            {
                _context.Espressoare.Remove(espressoare);
                await _context.SaveChangesAsync();
            }
        }
    }
}
