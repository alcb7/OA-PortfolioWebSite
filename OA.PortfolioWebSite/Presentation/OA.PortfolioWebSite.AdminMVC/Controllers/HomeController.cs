using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.AdminMVC.Models;
using System.Diagnostics;

namespace OA.PortfolioWebSite.AdminMVC.Controllers
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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Kullanýcý oturum açmýþ.");
            }
            else
            {
                Console.WriteLine("Kullanýcý oturum açmamýþ.");
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
