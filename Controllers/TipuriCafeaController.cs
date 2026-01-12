using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Services;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace mvc.Controllers
{
    public class TipuriCafeaController : Controller
    {
        private readonly ITipuriCafeaService _tipuriCafeaService;
        private readonly UserManager<Users> _userManager;

        public TipuriCafeaController(ITipuriCafeaService tipuriCafeaService, UserManager<Users> userManager)
        {
            _tipuriCafeaService = tipuriCafeaService;
            _userManager = userManager;

        }

        public async Task<IActionResult> Tipuri_cafea()
        {
            ViewData["Title"] = "Tipuri_cafea"; // Set the ViewData["Title"]
            var tipuriCafea = await _tipuriCafeaService.GetAllAsync();
            return View(tipuriCafea ?? new List<Tipuri_Cafea>());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _tipuriCafeaService.GetAllAsync();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _tipuriCafeaService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([Bind("Nume,Pret,Rating,Poza")] Tipuri_Cafea cafea, IFormFile? photoPathFile)
        {
            if (photoPathFile != null && photoPathFile.Length > 0)
            {
                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(photoPathFile.FileName).ToLower();

                if (photoPathFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("photoPathFile", "The file size cannot exceed 5MB.");
                }

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("photoPathFile", "Only JPG, JPEG and PNG files are allowed.");
                }

                if (ModelState.IsValid)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/tipuri_cafea");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photoPathFile.CopyToAsync(stream);
                    }

                    cafea.Poza = $"/uploads/tipuri_cafea/{uniqueFileName}";
                }
            }

            if (ModelState.IsValid)
            {
                await _tipuriCafeaService.AddAsync(cafea);
                return RedirectToAction(nameof(Index));
            }
            return View(cafea);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _tipuriCafeaService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("id,Nume,Pret,Rating,Poza")] Tipuri_Cafea cafea, IFormFile? photoPathFile)
        {
            if (id != cafea.id) return BadRequest();

            var existingItem = await _tipuriCafeaService.GetByIdAsync(id);
            if (existingItem == null) return NotFound();

            if (photoPathFile != null && photoPathFile.Length > 0)
            {
                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(photoPathFile.FileName).ToLower();

                if (photoPathFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("photoPathFile", "The file size cannot exceed 5MB.");
                }

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("photoPathFile", "Only JPG, JPEG and PNG files are allowed.");
                }

                if (ModelState.IsValid)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/tipuri_cafea");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photoPathFile.CopyToAsync(stream);
                    }

                    if (!string.IsNullOrEmpty(existingItem.Poza))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.Poza.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    cafea.Poza = $"/uploads/tipuri_cafea/{uniqueFileName}";
                }
            }
            else
            {
                cafea.Poza = existingItem.Poza;
            }

            if (ModelState.IsValid)
            {
                await _tipuriCafeaService.UpdateAsync(cafea);
                return RedirectToAction(nameof(Index));
            }
            return View(cafea);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _tipuriCafeaService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var existingItem = await _tipuriCafeaService.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(existingItem.Poza))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.Poza.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            await _tipuriCafeaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
