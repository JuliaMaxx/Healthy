using Healthy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Healthy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Entries");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Foods()
        {
            return View();
        }

        public IActionResult History()
        {
            return RedirectToAction("History", "Entries");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
