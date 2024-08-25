using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.UserMVC.ViewModels;
using System.Text;
using System.Text.Json;

namespace OA.PortfolioWebSite.UserMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://dataapi.digigokali.com.tr/api/PostContactMessage";


        public ContactController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(ContactMessageViewModel contactMessageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", contactMessageViewModel);
            }

            var jsonContent = JsonSerializer.Serialize(contactMessageViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // API'ye gönderim
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}api/ContactMessages", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Your message has been sent successfully.";
                return View("Index");
            }
            else
            {
                ModelState.AddModelError("", "There was an error sending your message. Please try again later.");
                return View("Index", contactMessageViewModel);
            }
        }

       
    }

}

