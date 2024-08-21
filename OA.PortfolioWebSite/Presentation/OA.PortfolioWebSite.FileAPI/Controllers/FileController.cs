using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.PortfolioWebSite.Application.Interfaces.Services;

namespace OA.PortfolioWebSite.FileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var directoryPath = Path.Combine("wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var safeFileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(directoryPath, safeFileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = "/images/" + safeFileName;

            return Ok(new { Url = fileUrl });
        }
        [HttpGet]
        public IActionResult GetFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File name is not provided");
            }

            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            var filePath = Path.Combine(rootPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var mimeType = "image/jpeg"; // Dosya uzantısına göre ayarlayın, örneğin: "image/png"
            return PhysicalFile(filePath, mimeType);
        }

        //private readonly IFileService _fileService;

        //public FileController(IFileService fileService)
        //{
        //    _fileService = fileService;
        //}
        //private readonly string _apiFileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "files");

        //[HttpPost]
        //[Route("upload")]
        //public async Task<IActionResult> Upload([FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("Dosya seçilmedi.");

        //    var filePath = Path.Combine(_apiFileStoragePath, file.FileName);

        //    if (!Directory.Exists(_apiFileStoragePath))
        //    {
        //        Directory.CreateDirectory(_apiFileStoragePath);
        //    }

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }

        //    return Ok(new { filePath });
        //}
        //[HttpGet]
        //public async Task<IActionResult> DownloadAsync([FromQuery] string fileName)
        //{
        //    try
        //    {
        //        var response = await _fileService.DownloadFileAsync(fileName);
        //        return File(response.FileContent, response.ContentType, response.FileName);
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest(e);
        //    }

        //}
    }
}
