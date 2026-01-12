using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace mvc.Controllers
{
    public class EspressoareController : Controller
    {
        private readonly IEspressoareService _espressoareService;
        private readonly UserManager<Users> _userManager;

        public EspressoareController(IEspressoareService espressoareService, UserManager<Users> userManager)
        {
            _espressoareService = espressoareService;
            _userManager = userManager;

        }

        public async Task<IActionResult> Espressoare()
        {
            ViewData["Title"] = "Espressoare"; // Set the ViewData["Title"]

            var espressoare = await _espressoareService.GetAllAsync();
            return View(espressoare ?? new List<Espressoare>());
        }

        public async Task<IActionResult> EspressorPage(int id)
        {
            ViewData["Title"] = "EspressorPage";
            var espressor = await _espressoareService.GetByIdAsync(id);
            if (espressor == null) return NotFound();
            return View(espressor);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _espressoareService.GetAllAsync();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _espressoareService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([Bind("Nume,Pret,Cod,Descriere,Stoc,Tip,Poza,PozaStanga,PozaDreapta,Video")] Espressoare espressoare, IFormFile? photoPathFile, IFormFile? photoStangaPathFile, IFormFile? photoDreaptaPathFile, IFormFile? videoPathFile)
        {
            espressoare.Poza = await SaveUploadedFileAsync(photoPathFile, "espressoare");
            espressoare.PozaStanga = await SaveUploadedFileAsync(photoStangaPathFile, "espressoare");
            espressoare.PozaDreapta = await SaveUploadedFileAsync(photoDreaptaPathFile, "espressoare");
            espressoare.Video = await SaveUploadedFileAsync(videoPathFile, "espressoare");

            if (ModelState.IsValid)
            {
                await _espressoareService.AddAsync(espressoare);
                return RedirectToAction(nameof(Index));
            }

            return View(espressoare);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {

            var item = await _espressoareService.GetByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("id,Nume,Pret,Cod,Descriere,Stoc,Tip,Poza,PozaStanga,PozaDreapta,Video")] Espressoare espressoare, IFormFile? photoPathFile, IFormFile? photoStangaPathFile, IFormFile? photoDreaptaPathFile, IFormFile? videoPathFile)
        {
            if (id != espressoare.id) return BadRequest();

            // Get the existing item from the database
            var existingItem = await _espressoareService.GetByIdAsync(id);
            if (existingItem == null) return NotFound();

            espressoare.Poza = await EditUploadedFileAsync(photoPathFile, "espressoare", existingItem.Poza);
            espressoare.PozaStanga = await EditUploadedFileAsync(photoStangaPathFile, "espressoare", existingItem.PozaStanga);
            espressoare.PozaDreapta = await EditUploadedFileAsync(photoDreaptaPathFile, "espressoare", existingItem.PozaDreapta);
            espressoare.Video = await EditUploadedFileAsync(videoPathFile, "espressoare", existingItem.Video);

            if (ModelState.IsValid)
            {
                await _espressoareService.UpdateAsync(espressoare);
                return RedirectToAction(nameof(Index));
            }
            return View(espressoare);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _espressoareService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var existingItem = await _espressoareService.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(existingItem.Poza))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.Poza.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            await _espressoareService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string?> SaveUploadedFileAsync(IFormFile? file, string uploadFolder)
        {
            if (file == null || file.Length == 0)
                return null;

            const long maxFileSize = 15 * 1024 * 1024; // 5MB
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".mp4", ".webm" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (file.Length > maxFileSize)
            {
                ModelState.AddModelError(file.Name, "The file size cannot exceed 5MB.");
                return null;
            }

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError(file.Name, "Only JPG, JPEG, PNG, MP4, and WEBM files are allowed.");
                return null;
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploads/{uploadFolder}");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{uploadFolder}/{uniqueFileName}";
        }

        private async Task<string?> EditUploadedFileAsync(
        IFormFile? file,
        string uploadFolder,
        string? existingFilePath)
        {
            if (file == null || file.Length == 0)
                return existingFilePath;

            const long maxFileSize = 15 * 1024 * 1024; // 15MB
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".mp4", ".webm" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (file.Length > maxFileSize)
            {
                ModelState.AddModelError(file.Name, "The file size cannot exceed 5MB.");
                return existingFilePath;
            }

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError(file.Name, "Only JPG, JPEG, PNG, MP4, and WEBM files are allowed.");
                return existingFilePath;
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/uploads/{uploadFolder}");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Delete old file if exists
            if (!string.IsNullOrEmpty(existingFilePath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingFilePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            return $"/uploads/{uploadFolder}/{uniqueFileName}";
        }
    }
}