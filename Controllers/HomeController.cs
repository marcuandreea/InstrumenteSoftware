using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       
        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact"; // Set the ViewData["Title"]
            return View();
        }

        public IActionResult Abonament()
        {
            ViewData["Title"] = "Abonament"; // Set the ViewData["Title"]
            return View();
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Index"; // Set the ViewData["Title"]
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

        public IActionResult Recenzii()
        {
            ViewData["Title"] = "Recenzii"; // Set the ViewData["Title"]
            return View();
        }

        public IActionResult Rivelia()
        {
            ViewData["Title"] = "Rivelia"; // Set the ViewData["Title"]
            return View();
        }

        public IActionResult Tipuri_cafea()
        {
            ViewData["Title"] = "Tipuri_cafea"; // Set the ViewData["Title"]
            return View();
        }

        public IActionResult Espressoare()
        {
            ViewData["Title"] = "Espressoare"; // Set the ViewData["Title"]
            return View();
        }

        public IActionResult User()
        {
            ViewData["Title"] = "User"; // Set the ViewData["Title"]
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
