using Microsoft.EntityFrameworkCore;
using mvc.Models;
using mvc.Data;

namespace mvc.IRepository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSubscriptionAsync(Abonament subscription)
        {
            await _context.Abonament.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Abonament>> GetAllSubscriptionsAsync()
        {
            return await _context.Abonament.ToListAsync();
        }

        public async Task<Abonament?> GetSubscriptionByIdAsync(int id)
        {
            return await _context.Abonament.FindAsync(id);
        }

        public async Task UpdateSubscriptionAsync(Abonament subscription)
        {
            _context.Abonament.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscriptionAsync(int id)
        {
            var subscription = await _context.Abonament.FindAsync(id);
            if (subscription != null)
            {
                _context.Abonament.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }

}
