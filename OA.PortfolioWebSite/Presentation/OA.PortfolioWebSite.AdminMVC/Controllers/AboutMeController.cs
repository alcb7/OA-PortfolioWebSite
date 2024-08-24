using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.AdminMVC.ViewModels;

namespace OA.PortfolioWebSite.AdminMVC.Controllers
{
    [Authorize(Roles = "admin")]
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
           // aboutMe.ImageUrl1 = Path.GetFileName(aboutMe.ImageUrl1);
            //aboutMe.ImageUrl2 = Path.GetFileName(aboutMe.ImageUrl2);
            aboutMe.ImageUrl1 = $"https://localhost:7051/api/File?fileName={Path.GetFileName(aboutMe.ImageUrl1)}";
            aboutMe.ImageUrl2 = $"https://localhost:7051/api/File?fileName={Path.GetFileName(aboutMe.ImageUrl2)}";


            return View(aboutMe);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutMe = await _httpClient.GetFromJsonAsync<AboutMeViewModel>($"{_apiBaseUrl}/{id}");
            aboutMe.ImageUrl1 = $"https://localhost:7051/api/File?fileName={Path.GetFileName(aboutMe.ImageUrl1)}";
            aboutMe.ImageUrl2 = $"https://localhost:7051/api/File?fileName={Path.GetFileName(aboutMe.ImageUrl2)}";
            return View(aboutMe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AboutMeViewModel aboutMe, IFormFile image1, IFormFile image2)
        {
            if (id != aboutMe.Id)
            {
                return BadRequest("ID in the URL does not match ID in the form");
            }

            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    var response = await _httpClient.PostAsync("https://localhost:7051/api/File/upload", new MultipartFormDataContent
            {
                { new StreamContent(image1.OpenReadStream()), "file", image1.FileName }
            });

                    if (response.IsSuccessStatusCode)
                    {
                        var fileResult = await response.Content.ReadFromJsonAsync<FileUploadResult>();
                        aboutMe.ImageUrl1 = fileResult.Url;
                    }
                }

                if (image2 != null)
                {
                    var response = await _httpClient.PostAsync("https://localhost:7051/api/File/upload", new MultipartFormDataContent
            {
                { new StreamContent(image2.OpenReadStream()), "file", image2.FileName }
            });

                    if (response.IsSuccessStatusCode)
                    {
                        var fileResult = await response.Content.ReadFromJsonAsync<FileUploadResult>();
                        aboutMe.ImageUrl2 = fileResult.Url;
                    }
                }

                var updateResponse = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", aboutMe);

                if (updateResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(aboutMe);
        }

        public class FileUploadResult
        {
            public string Url { get; set; }
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
