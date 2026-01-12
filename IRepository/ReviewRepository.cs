using mvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using mvc.Data;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Review.ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Review.FindAsync(id);
    }

    public async Task AddAsync(Review review)
    {
        _context.Review.Add(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review review)
    {
        _context.Review.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var review = await _context.Review.FindAsync(id);
        if (review != null)
        {
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
