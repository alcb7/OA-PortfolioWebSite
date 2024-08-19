using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.UserMVC.Models;
using OA.PortfolioWebSite.UserMVC.ViewModels;
using System.Diagnostics;

namespace OA.PortfolioWebSite.UserMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiProjectsUrl = "https://localhost:7260/api/PersonalInfo/1";

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            var projectResponse = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>(_apiProjectsUrl);

            
            return View(projectResponse);
        }
    }
}
