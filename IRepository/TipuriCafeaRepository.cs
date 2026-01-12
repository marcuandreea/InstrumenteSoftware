using mvc.Models;
using mvc.IRepository;
using mvc.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Repository
{
    public class TipuriCafeaRepository : ITipuriCafeaRepository
    {
        private readonly AppDbContext _context;

        public TipuriCafeaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipuri_Cafea>> GetAllAsync()
        {
            return await _context.Tipuri_Cafea.ToListAsync();
        }

        public async Task<Tipuri_Cafea?> GetByIdAsync(int id)
        {
            return await _context.Tipuri_Cafea.AsNoTracking().FirstOrDefaultAsync(e => e.id == id);
        }

        public async Task AddAsync(Tipuri_Cafea cafea)
        {
            _context.Tipuri_Cafea.Add(cafea);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tipuri_Cafea cafea)
        {
            _context.Tipuri_Cafea.Update(cafea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cafea = await _context.Tipuri_Cafea.FindAsync(id);
            if (cafea != null)
            {
                _context.Tipuri_Cafea.Remove(cafea);
                await _context.SaveChangesAsync();
            }
        }
    }
}
