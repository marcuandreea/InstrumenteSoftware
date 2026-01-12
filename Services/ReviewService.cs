using mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;

    public ReviewService(IReviewRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Review>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Review?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Review review) => _repository.AddAsync(review);

    public Task UpdateAsync(Review review) => _repository.UpdateAsync(review);

    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}
