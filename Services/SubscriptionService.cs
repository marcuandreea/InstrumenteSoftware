using mvc.IRepository;
using mvc.Models;

namespace mvc.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUserRepository userRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
        }

        public async Task<Abonament?> GetSubscriptionByIdAsync(int id)
        {
            return await _subscriptionRepository.GetSubscriptionByIdAsync(id);
        }

        public async Task UpdateSubscriptionAsync(Abonament subscription)
        {
            var existingSubscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscription.id);
            if (existingSubscription == null)
            {
                throw new ArgumentException("Subscription not found.");
            }

            // Update the subscription properties
            existingSubscription.Tip_Abonament = subscription.Tip_Abonament;

            await _subscriptionRepository.UpdateSubscriptionAsync(existingSubscription);
        }

        public async Task AddSubscriptionAsync(Abonament subscription)
        {
            await _subscriptionRepository.AddSubscriptionAsync(subscription);
        }

        public async Task<IEnumerable<Abonament>> GetAllSubscriptionsAsync()
        {
            return await _subscriptionRepository.GetAllSubscriptionsAsync();
        }

        public async Task DeleteSubscriptionAsync(int id)
        {
            await _subscriptionRepository.DeleteSubscriptionAsync(id);
        }
    }
}
