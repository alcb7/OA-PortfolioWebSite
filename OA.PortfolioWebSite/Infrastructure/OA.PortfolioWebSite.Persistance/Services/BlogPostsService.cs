using Ardalis.Result;
using AutoMapper;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class BlogPostsService : IBlogPostsService
    {
        private readonly IBlogPostsRepository _repository;
        private readonly IMapper _mapper;

        public BlogPostsService(IBlogPostsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<BlogPosts>>> GetAllBlogPostsAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (!entities.IsSuccess)
            {
                return Result<IEnumerable<BlogPosts>>.Invalid(entities.ValidationErrors);
            }
            return entities;
        }

        public async Task<Result<BlogPosts>> GetBlogPostsByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (!entity.IsSuccess)
            {
                return Result<BlogPosts>.Invalid(entity.ValidationErrors);
            }
            return entity;
        }

        public async Task<Result<BlogPosts>> AddBlogPostsAsync(BlogPostsCreateDto dto)
        {
            var entity = _mapper.Map<BlogPosts>(dto);
            var result = await _repository.AddAsync(entity);

            if (!result.IsSuccess)
            {
                return Result<BlogPosts>.Invalid(result.ValidationErrors);
            }
            return result;
        }

        public async Task<Result<BlogPosts>> UpdateBlogPostsAsync(BlogPostsUpdateDto dto)
        {
            var result = await _repository.GetByIdAsync(dto.Id);
            if (!result.IsSuccess)
            {
                return Result<BlogPosts>.Invalid(result.ValidationErrors);
            }

            _mapper.Map(dto, result.Value);
            return await _repository.UpdateAsync(result.Value);
        }

        public async Task<Result> DeleteBlogPostsAsync(int id)
        {
            var experienceResult = await _repository.GetByIdAsync(id);
            if (!experienceResult.IsSuccess)
            {
                return Result.Invalid(experienceResult.ValidationErrors);
            }

            return await _repository.DeleteAsync(id);
        }
        public async Task<Result<IEnumerable<BlogPosts>>> GetBlogPostsByUserIdAsync(int userId)
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess)
            {
                var errors = string.Join("; ", result.Errors);
                return Result<IEnumerable<BlogPosts>>.Error(errors);
            }

            var userExperiences = result.Value.Where(e => e.AuthorId == userId);
            return Result<IEnumerable<BlogPosts>>.Success(userExperiences);
        }
    }
}
