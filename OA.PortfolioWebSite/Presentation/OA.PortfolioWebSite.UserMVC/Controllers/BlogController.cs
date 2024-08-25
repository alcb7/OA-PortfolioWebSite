using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.UserMVC.ViewModels;

namespace OA.PortfolioWebSite.UserMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBlogPostsUrl = "https://localhost:7260/api/BlogPosts";

        public BlogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Details(int id)
        {
            var blogPosts = await _httpClient.GetFromJsonAsync<List<BlogPostsViewModel>>(_apiBlogPostsUrl);
            foreach (var blog in blogPosts)
            {
                blog.ImageUrl = $"https://localhost:7051/api/File?fileName={Path.GetFileName(blog.ImageUrl)}";
            }
            var blogPost = blogPosts.FirstOrDefault(x => x.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }
    }
}
