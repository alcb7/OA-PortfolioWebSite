using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.AdminMVC.ViewModels;

namespace OA.PortfolioWebSite.AdminMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class PersonelInfoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7260/api/PersonelInfo";

        public PersonelInfoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>(_apiBaseUrl + "/1");
            
            // Her proje için ImageUrl'yi tam URL'ye çeviriyoruz


            return View(projects);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _httpClient.GetFromJsonAsync<PersonelInfoViewModel>($"{_apiBaseUrl}/{id}");

            // Sadece dosya adını almak için Path.GetFileName kullanıyoruz
            



            return View(project);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, PersonelInfoViewModel project, IFormFile image)
        {
            if (id != project.Id)
            {
                return BadRequest("ID in the URL does not match ID in the form");
            }

            if (ModelState.IsValid)
            {
                

                var updateResponse = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", project);

                if (updateResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(project);
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
