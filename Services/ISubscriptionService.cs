using mvc.Models;

namespace mvc.Services
{
    public interface ISubscriptionService
    {
        Task<Abonament?> GetSubscriptionByIdAsync(int id);
        Task<IEnumerable<Abonament>> GetAllSubscriptionsAsync();
        Task AddSubscriptionAsync(Abonament subscription);
        Task UpdateSubscriptionAsync(Abonament subscription);
        Task DeleteSubscriptionAsync(int id);
    }
}
