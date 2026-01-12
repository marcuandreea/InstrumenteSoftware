using mvc.Models;
using mvc.IRepository;
using mvc.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Repository
{
    public class CosCumparaturiRepository : ICosCumparaturiRepository
    {
        private readonly AppDbContext _context;

        public CosCumparaturiRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cos_cumparaturi>> GetAllAsync()
        {
            return await _context.Cos_cumparaturi
                .Include(c => c.Users)
                .Include(c => c.Tipuri_Cafea)
                .Include(c => c.Espressoare)
                .ToListAsync();
        }

        public async Task<Cos_cumparaturi?> GetByIdAsync(int id)
        {
            return await _context.Cos_cumparaturi
                .Include(c => c.Users)
                .Include(c => c.Tipuri_Cafea)
                .Include(c => c.Espressoare)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task AddAsync(Cos_cumparaturi cos)
        {
            _context.Cos_cumparaturi.Add(cos);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cos_cumparaturi cos)
        {
            _context.Cos_cumparaturi.Update(cos);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cos = await _context.Cos_cumparaturi.FindAsync(id);
            if (cos != null)
            {
                _context.Cos_cumparaturi.Remove(cos);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cos_cumparaturi>> GetByUserIdAsync(string userId)
        {
            return await _context.Cos_cumparaturi
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }
    }
}
