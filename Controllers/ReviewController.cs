using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace mvc.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<Users> _userManager;

        public ReviewController(IReviewService reviewService, UserManager<Users> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;

        }

        public async Task<IActionResult> Recenzii()
        {
            ViewData["Title"] = "Recenzii"; // Set the ViewData["Title"]

            var model = new ReviewViewModel
            {
                Review = (await _reviewService.GetAllAsync()).ToList(),
                Users = (await _userManager.Users.ToListAsync()).ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllAsync();
            return View(reviews);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();
            return View(review);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("id,userID,nota,review")] Review model)
        {
            if (ModelState.IsValid)
            {
                await _reviewService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("id,userID,nota,review")] Review model)
        {
            if (id != model.id) return BadRequest();
            if (ModelState.IsValid)
            {
                await _reviewService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();
            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reviewService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Abonat,Vizitator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview([Bind("id,userID,nota,review")] Review model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to login page, returnUrl will bring user back to Cart after login
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = Url.Action("Recenzii", "Review") });
            }

            if (ModelState.IsValid)
            {
                await _reviewService.AddAsync(model);
                return RedirectToAction(nameof(Recenzii));
            }
            return View(model);
        }
    }
}
