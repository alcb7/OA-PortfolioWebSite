using Microsoft.AspNetCore.Http;
using OA.PortfolioWebSite.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface IFileService
    {
        List<string> GetFiles();
        Task UploadFileAsync(IFormFile formFile);

        Task<FileResponse?> DownloadFileAsync(string fileName);

        Task DeleteAsync(string fileName);
    }
}
