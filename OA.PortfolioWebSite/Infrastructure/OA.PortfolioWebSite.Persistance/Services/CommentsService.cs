using Ardalis.Result;
using AutoMapper;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _repository;
        private readonly IMapper _mapper;

        public CommentsService(ICommentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<Comments>>> GetAllCommentsAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (!entities.IsSuccess)
            {
                return Result<IEnumerable<Comments>>.Invalid(entities.ValidationErrors);
            }
            return entities;
        }

        public async Task<Result<Comments>> GetCommentsByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (!entity.IsSuccess)
            {
                return Result<Comments>.Invalid(entity.ValidationErrors);
            }
            return entity;
        }

        public async Task<Result<Comments>> AddCommentsAsync(CommentsCreateDto dto)
        {
            var entity = _mapper.Map<Comments>(dto);
            var result = await _repository.AddAsync(entity);

            if (!result.IsSuccess)
            {
                return Result<Comments>.Invalid(result.ValidationErrors);
            }
            return result;
        }

        public async Task<Result<Comments>> UpdateCommentsAsync(CommentsUpdateDto dto)
        {
            var result = await _repository.GetByIdAsync(dto.Id);
            if (!result.IsSuccess)
            {
                return Result<Comments>.Invalid(result.ValidationErrors);
            }

            _mapper.Map(dto, result.Value);
            return await _repository.UpdateAsync(result.Value);
        }

        public async Task<Result> DeleteCommentsAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return Result.Invalid(result.ValidationErrors);
            }

            return await _repository.DeleteAsync(id);
        }
    }
}

