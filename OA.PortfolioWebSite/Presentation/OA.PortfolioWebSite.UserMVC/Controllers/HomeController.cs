using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.UserMVC.Models;
using OA.PortfolioWebSite.UserMVC.ViewModels;
using System.Diagnostics;

namespace OA.PortfolioWebSite.UserMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiPersonelInfoUrl = "https://localhost:7260/api/PersonalInfo/1";
        private readonly string _apiAboutMeUrl = "https://localhost:7260/api/AboutMe/1";

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            // PersonelInfo verisini API'den çekiyoruz.
            var personelInfoResponse = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>(_apiPersonelInfoUrl);

            // AboutMe verisini API'den çekiyoruz.
            var aboutMeResponse = await _httpClient.GetFromJsonAsync<AboutMeViewModel>(_apiAboutMeUrl);

            // ViewModel oluþturup, her iki veriyi de set ediyoruz.
            var viewModel = new HomeViewModel
            {
                PersonelInfo = personelInfoResponse,
                AboutMe = aboutMeResponse
            };

            return View(viewModel);
        }
    }
}
