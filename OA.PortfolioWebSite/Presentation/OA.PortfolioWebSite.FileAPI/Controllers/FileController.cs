using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OA.PortfolioWebSite.FileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            var path = Path.Combine("wwwroot/images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"https://localhost:7051/images/{file.FileName}"; // Burada localhost:5001 yerine senin API'nin domainini kullanmalısın.
            return Ok(fileUrl);
        }
    }
}
