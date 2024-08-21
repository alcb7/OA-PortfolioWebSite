using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class FileService : IFileService
    {
        private readonly string _rootPath;
        private readonly IConfiguration _configuration;
        private ILogger<FileService> _logger;

        public FileService(IConfiguration configuration, ILogger<FileService> logger, IHostingEnvironment environment)
        {
            _rootPath = Path.Combine(environment.ContentRootPath, "Uploads");
            _configuration = configuration;
            _logger = logger;
        }



        public async Task DeleteAsync(string fileName)
        {
            await Task.Run(() =>
            {
                var uploadFolder = GetUploadFolder();
                var fullFilePath = Path.Combine(uploadFolder, fileName);
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
            });
        }

        public async Task<FileResponse?> DownloadFileAsync(string fileName)
        {
            var uploadFolder = GetUploadFolder();
            var fullFilePath = Path.Combine(uploadFolder, fileName);
            if (!File.Exists(fullFilePath))
            {
                return null;
            }
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fullFilePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return new FileResponse
            {
                FileName = fileName,
                ContentType = contentType,
                FileContent = await File.ReadAllBytesAsync(fullFilePath)
            };
        }

        public List<string> GetFiles()
        {
            var uploadFolder = GetUploadFolder();

            var files = Directory.GetFiles(uploadFolder);

            return files.Select(f => Path.GetFileName(f)).ToList();
        }

        public async Task UploadFileAsync(IFormFile formFile)
        {
            var uploadFolder = GetUploadFolder();

            var fullFilePath = Path.Combine(uploadFolder, formFile.FileName);

            using var fileStream = new FileStream(fullFilePath, FileMode.Create);

            await formFile.CopyToAsync(fileStream);

            _logger.LogInformation("File uploaded");
        }


        private string GetUploadFolder()
        {
            var localUploadFolder = _configuration.GetSection("FileUploadLocation").Value;
            if (string.IsNullOrWhiteSpace(localUploadFolder))
            {
                throw new InvalidOperationException("FileUploadLocation is not configured.");
            }
            var fullPath = Path.Combine(_rootPath, localUploadFolder);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            return fullPath;
        }
    }
}
