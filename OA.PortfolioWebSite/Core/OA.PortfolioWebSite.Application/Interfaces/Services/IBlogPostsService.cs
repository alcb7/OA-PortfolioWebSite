using Ardalis.Result;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface IBlogPostsService
    {
        Task<Result<IEnumerable<BlogPosts>>> GetAllBlogPostsAsync();
        Task<Result<BlogPosts>> GetBlogPostsByIdAsync(int id);
        Task<Result<BlogPosts>> AddBlogPostsAsync(BlogPostsCreateDto experienceCreateDto);
        Task<Result<BlogPosts>> UpdateBlogPostsAsync(BlogPostsUpdateDto experienceUpdateDto);
        Task<Result> DeleteBlogPostsAsync(int id);
        Task<Result<IEnumerable<BlogPosts>>> GetBlogPostsByUserIdAsync(int userId);
    }
}
