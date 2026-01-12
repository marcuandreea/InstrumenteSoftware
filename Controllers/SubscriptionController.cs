using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly UserManager<Users> _userManager;

        public SubscriptionController(ISubscriptionService subscriptionService, UserManager<Users> userManager)
        {
            _subscriptionService = subscriptionService;
            _userManager = userManager;
        }

        // GET: Subscription
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            return View(subscriptions);
        }

        // GET: Subscription/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View("Details", subscription!); // Use null-forgiving operator
        }

        // GET: Subscription/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subscription/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Tip_Abonament")] Abonament subscription)
        {
            if (ModelState.IsValid)
            {

                await _subscriptionService.AddSubscriptionAsync(subscription);
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }

        // GET: Subscription/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request.");
            }

            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }

        // POST: Subscription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("id,Tip_Abonament")] Abonament subscription)
        {
            if (id != subscription.id)
            {
                return BadRequest("Subscription ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                await _subscriptionService.UpdateSubscriptionAsync(subscription);
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }



        // GET: Subscription/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }

        // POST: Subscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subscriptionService.DeleteSubscriptionAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Abonament()
        {
            ViewData["Title"] = "Abonament";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Suporter,Vizitator")]
        public async Task<IActionResult> UpdateSubscriptionType(string subscriptionType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Generate a new subscription ID if the current ID is 0
            if (user.ID_abonament == 0)
            {
                var newSubscription = new Abonament
                {
                    Tip_Abonament = subscriptionType,
                };

                // Add the new subscription and get the generated ID
                await _subscriptionService.AddSubscriptionAsync(newSubscription);
                user.ID_abonament = newSubscription.id;

                // Update the user with the new subscription ID
                await _userManager.UpdateAsync(user);
            }
            else
            {
                // Update the existing subscription
                var subscription = new Abonament
                {
                    id = user.ID_abonament,
                    Tip_Abonament = subscriptionType,
                };

                await _subscriptionService.UpdateSubscriptionAsync(subscription);
            }

            return RedirectToAction("User", "Users");
        }
    }
}