using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Services;
using mvc.ViewModels;
using System.Threading.Tasks;

namespace mvc.Controllers
{
    public class CosCumparaturiController : Controller
    {
        private readonly ICosCumparaturiService _cosCumparaturiService;
        private readonly ITipuriCafeaService _tipuriCafeaService;
        private readonly IEspressoareService _espressoareService;
        private readonly UserManager<Users> _userManager;

        public CosCumparaturiController(
            ICosCumparaturiService cosCumparaturiService,
            ITipuriCafeaService tipuriCafeaService,
            IEspressoareService espressoareService,
            UserManager<Users> userManager)
        {
            _cosCumparaturiService = cosCumparaturiService;
            _tipuriCafeaService = tipuriCafeaService;
            _espressoareService = espressoareService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin,Abonat,Vizitator")]
        public async Task<IActionResult> Cart()
        {
            var userId = _userManager.GetUserId(User); // Correct usage of _userManager
            var model = new CartViewModel
            {
                Espressoare = (await _espressoareService.GetAllAsync()).ToList(),
                TipuriCafea = (await _tipuriCafeaService.GetAllAsync()).ToList(),
                CosCumparaturi = (await _cosCumparaturiService.GetByUserIdAsync(userId)).ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _cosCumparaturiService.GetAllAsync();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _cosCumparaturiService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("UserID,IDCafea,CodEspressor")] Cos_cumparaturi cos)
        {
            if (ModelState.IsValid)
            {
                await _cosCumparaturiService.AddAsync(cos);
                return RedirectToAction(nameof(Index));
            }
            return View(cos);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _cosCumparaturiService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("id,UserID,IDCafea,CodEspressor")] Cos_cumparaturi cos)
        {
            if (id != cos.id) return BadRequest();
            if (ModelState.IsValid)
            {
                await _cosCumparaturiService.UpdateAsync(cos);
                return RedirectToAction(nameof(Index));
            }
            return View(cos);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _cosCumparaturiService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cosCumparaturiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = _userManager.GetUserId(User);
            var items = await _cosCumparaturiService.GetByUserIdAsync(userId);
            foreach (var item in items)
            {
                await _cosCumparaturiService.DeleteAsync(item.id);
            }
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost, ActionName("DeleteFromCart")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Abonat,Vizitator")]
        public async Task<IActionResult> DeleteFromCart(int id)
        {
            await _cosCumparaturiService.DeleteAsync(id);
            var userId = _userManager.GetUserId(User);
            var model = new CartViewModel
            {
                Espressoare = (await _espressoareService.GetAllAsync()).ToList(),
                TipuriCafea = (await _tipuriCafeaService.GetAllAsync()).ToList(),
                CosCumparaturi = (await _cosCumparaturiService.GetByUserIdAsync(userId)).ToList()
            };
            return View("Cart", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Abonat,Vizitator")]
        public async Task<IActionResult> AddToCart([Bind("UserID,IDCafea,CodEspressor")] Cos_cumparaturi cos)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to login page, returnUrl will bring user back to Cart after login
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = Url.Action("Cart", "CosCumparaturi") });
            }

            if (ModelState.IsValid)
            {
                await _cosCumparaturiService.AddAsync(cos);
                return RedirectToAction(nameof(Cart));
            }
            return View(cos);
        }
    }
}
