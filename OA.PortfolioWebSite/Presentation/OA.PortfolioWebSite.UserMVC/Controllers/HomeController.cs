using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.UserMVC.Models;
using OA.PortfolioWebSite.UserMVC.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace OA.PortfolioWebSite.UserMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiPersonelInfoUrl = "https://localhost:7260/api/PersonalInfo/1";
        private readonly string _apiAboutMeUrl = "https://localhost:7260/api/AboutMe/1";
        private readonly string _apiEducationsUrl = "https://localhost:7260/api/Educations";
        private readonly string _apiContactUrl = "https://localhost:7260/api/PostContactMessage"; // Yeni eklenen API URL

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            var personelInfoResponse = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>(_apiPersonelInfoUrl);
            var aboutMeResponse = await _httpClient.GetFromJsonAsync<AboutMeViewModel>(_apiAboutMeUrl);
            var educationsResponse = await _httpClient.GetFromJsonAsync<List<EducationsViewModel>>(_apiEducationsUrl);

            // Sadece dosya adýný almak için Path.GetFileName kullanýyoruz
            aboutMeResponse.ImageUrl1 = Path.GetFileName(aboutMeResponse.ImageUrl1);
            aboutMeResponse.ImageUrl2 = Path.GetFileName(aboutMeResponse.ImageUrl2);

            var viewModel = new HomeViewModel
            {
                PersonelInfo = personelInfoResponse,
                AboutMe = aboutMeResponse,
                Educations = educationsResponse
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadCV()
        {
            var apiUrl = "https://localhost:7051/api/file/download-cv";

            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var fileBytes = await response.Content.ReadAsByteArrayAsync();
            return File(fileBytes, "application/pdf", "alirizacanbulan_cv.pdf");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactMessageViewModel contactMessageViewModel)
        {
            if (!ModelState.IsValid)
            {


                // Hatalý durumda HomeViewModel ile geri dönüyoruz
                var personelInfoResponse = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>(_apiPersonelInfoUrl);
                var aboutMeResponse = await _httpClient.GetFromJsonAsync<AboutMeViewModel>(_apiAboutMeUrl);
                var educationsResponse = await _httpClient.GetFromJsonAsync<List<EducationsViewModel>>(_apiEducationsUrl);
                aboutMeResponse.ImageUrl1 = Path.GetFileName(aboutMeResponse.ImageUrl1);
                aboutMeResponse.ImageUrl2 = Path.GetFileName(aboutMeResponse.ImageUrl2);

                var viewModel = new HomeViewModel
                {
                    Contactmessage = contactMessageViewModel,
                    PersonelInfo = personelInfoResponse,
                    AboutMe = aboutMeResponse,
                    Educations = educationsResponse
                };
                return View("Index", viewModel);
            }

            // JSON içeriðini oluþturuyoruz
            var jsonContent = JsonSerializer.Serialize(contactMessageViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // API'ye POST isteði gönder
            var response = await _httpClient.PostAsync(_apiContactUrl, content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Your message has been sent successfully.";
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Error response: {errorResponse}");
                ModelState.AddModelError("", "There was an error sending your message. Please try again later.");
            }

            // HomeViewModel ile Index view'ine geri dönüyoruz
            var updatedViewModel = new HomeViewModel
            {
                Contactmessage = contactMessageViewModel,
                PersonelInfo = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>(_apiPersonelInfoUrl),
                AboutMe = await _httpClient.GetFromJsonAsync<AboutMeViewModel>(_apiAboutMeUrl),
                Educations = await _httpClient.GetFromJsonAsync<List<EducationsViewModel>>(_apiEducationsUrl)
            };

            return View("Index", updatedViewModel);
        }


    }

}


