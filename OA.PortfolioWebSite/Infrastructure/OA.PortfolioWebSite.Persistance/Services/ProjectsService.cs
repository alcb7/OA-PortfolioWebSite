using Ardalis.Result;
using AutoMapper;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Domain.Entities;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _repository;
        private readonly IMapper _mapper;

        public ProjectsService(IProjectsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<Projects>>> GetAllProjectsAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (!entities.IsSuccess)
            {
                return Result<IEnumerable<Projects>>.Invalid(entities.ValidationErrors);
            }
            return entities;
        }

        public async Task<Result<Projects>> GetProjectsByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (!entity.IsSuccess)
            {
                return Result<Projects>.Invalid(entity.ValidationErrors);
            }
            return entity;
        }

        public async Task<Result<Projects>> AddProjectsAsync(ProjectsCreateDto dto)
        {
            var entity = _mapper.Map<Projects>(dto);
            var result = await _repository.AddAsync(entity);

            if (!result.IsSuccess)
            {
                return Result<Projects>.Invalid(result.ValidationErrors);
            }
            return result;
        }

        public async Task<Result<Projects>> UpdateProjectsAsync(ProjectsUpdateDto dto)
        {
            var result = await _repository.GetByIdAsync(dto.Id);
            if (!result.IsSuccess)
            {
                return Result<Projects>.Invalid(result.ValidationErrors);
            }

            _mapper.Map(dto, result.Value);
            return await _repository.UpdateAsync(result.Value);
        }

        public async Task<Result> DeleteProjectsAsync(int id)
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
