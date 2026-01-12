using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Data;
using mvc.Models;
using mvc.ViewModels;
namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public HomeController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Index"; // Set the ViewData["Title"]
            return View();
        }
        public IActionResult AccessDenied()
        {
            ViewData["Title"] = "AccessDenied"; // Set the ViewData["Title"]
            return View();
        }
        public IActionResult Intrebari()
        {
            ViewData["Title"] = "Intrebari"; // Set the ViewData["Title"]
            return View();
        }

        public IActionResult Poveste()
        {
            ViewData["Title"] = "Poveste"; // Set the ViewData["Title"]
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
