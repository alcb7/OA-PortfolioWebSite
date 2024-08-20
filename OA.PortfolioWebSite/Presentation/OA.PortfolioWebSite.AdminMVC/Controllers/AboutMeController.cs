using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.AdminMVC.ViewModels;

namespace OA.PortfolioWebSite.AdminMVC.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7260/api/AboutMe";

        public AboutMeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var aboutMe = await _httpClient.GetFromJsonAsync<AboutMeViewModel>(_apiBaseUrl + "/1");
            return View(aboutMe);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutMe = await _httpClient.GetFromJsonAsync<AboutMeViewModel>($"{_apiBaseUrl}/{id}");
            return View(aboutMe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutMeViewModel aboutMe, IFormFile image1, IFormFile image2)
        {
            if (id != aboutMe.Id)
            {
                return BadRequest("ID in the URL does not match ID in the form");
            }

            // Dosya yolu
            var directoryPath = Path.Combine("wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    var safeFileName = Path.GetFileName(image1.FileName); 
                    var filePath = Path.Combine(directoryPath, safeFileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await image1.CopyToAsync(stream);
                    }
                    aboutMe.ImageUrl1 = "/images/" + safeFileName;
                }

                if (image2 != null)
                {
                    var safeFileName = Path.GetFileName(image2.FileName); // Geçersiz karakterleri kaldırır
                    var filePath = Path.Combine(directoryPath, safeFileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await image2.CopyToAsync(stream);
                    }
                    aboutMe.ImageUrl2 = "/images/" + safeFileName;
                }

                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", aboutMe);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(aboutMe);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest("Unable to delete the record");
        }
    }


}
