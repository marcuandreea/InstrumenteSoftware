using mvc.Models;

namespace mvc.IRepository
{
    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(Abonament subscription);
        Task<IEnumerable<Abonament>> GetAllSubscriptionsAsync();
        Task<Abonament?> GetSubscriptionByIdAsync(int id);
        Task UpdateSubscriptionAsync(Abonament subscription);
        Task DeleteSubscriptionAsync(int id);
    }
}
