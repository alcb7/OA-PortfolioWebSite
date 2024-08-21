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
            var personelInfoResponse = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>(_apiPersonelInfoUrl);
            var aboutMeResponse = await _httpClient.GetFromJsonAsync<AboutMeViewModel>(_apiAboutMeUrl);

            // Sadece dosya adýný almak için Path.GetFileName kullanýyoruz
            aboutMeResponse.ImageUrl1 = Path.GetFileName(aboutMeResponse.ImageUrl1);
            aboutMeResponse.ImageUrl2 = Path.GetFileName(aboutMeResponse.ImageUrl2);

            var viewModel = new HomeViewModel
            {
                PersonelInfo = personelInfoResponse,
                AboutMe = aboutMeResponse
            };

            return View(viewModel);
        }
    }
}
