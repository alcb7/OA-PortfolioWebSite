using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.AdminMVC.ViewModels;

namespace OA.PortfolioWebSite.AdminMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProjectsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7260/api/Projects";

        public ProjectsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _httpClient.GetFromJsonAsync<List<ProjectViewModel>>(_apiBaseUrl);

            // Her proje için ImageUrl'yi tam URL'ye çeviriyoruz
            foreach (var project in projects)
            {
                project.ImageUrl = $"https://localhost:7051/api/File?fileName={Path.GetFileName(project.ImageUrl)}";
            }

            return View(projects);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _httpClient.GetFromJsonAsync<ProjectViewModel>($"{_apiBaseUrl}/{id}");

            // Sadece dosya adını almak için Path.GetFileName kullanıyoruz
            project.ImageUrl = Path.GetFileName(project.ImageUrl);

            

            return View(project);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProjectViewModel project, IFormFile image)
        {
            if (id != project.Id)
            {
                return BadRequest("ID in the URL does not match ID in the form");
            }

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var response = await _httpClient.PostAsync("https://localhost:7051/api/File/upload", new MultipartFormDataContent
                    {
                        { new StreamContent(image.OpenReadStream()), "file", image.FileName }
                    });

                    if (response.IsSuccessStatusCode)
                    {
                        var fileResult = await response.Content.ReadFromJsonAsync<FileUploadResult>();
                        project.ImageUrl = fileResult.Url;
                    }
                }

                var updateResponse = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", project);

                if (updateResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(project);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel project, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var response = await _httpClient.PostAsync("https://localhost:7051/api/File/upload", new MultipartFormDataContent
                    {
                        { new StreamContent(image.OpenReadStream()), "file", image.FileName }
                    });

                    if (response.IsSuccessStatusCode)
                    {
                        var fileResult = await response.Content.ReadFromJsonAsync<FileUploadResult>();
                        project.ImageUrl = fileResult.Url;
                    }
                }

                var createResponse = await _httpClient.PostAsJsonAsync(_apiBaseUrl, project);

                if (createResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(project);
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
