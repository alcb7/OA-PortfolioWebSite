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
    public class EducationsService : IEducationService
    {
        private readonly IEducationRepository _repository;
        private readonly IMapper _mapper;

        public EducationsService(IEducationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<Educations>>> GetAllEducationsAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (!entities.IsSuccess)
            {
                return Result<IEnumerable<Educations>>.Invalid(entities.ValidationErrors);
            }
            return entities;
        }

        public async Task<Result<Educations>> GetEducationsByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (!entity.IsSuccess)
            {
                return Result<Educations>.Invalid(entity.ValidationErrors);
            }
            return entity;
        }

        public async Task<Result<Educations>> AddEducationsAsync(EducationsCreateDto dto)
        {
            var entity = _mapper.Map<Educations>(dto);
            var result = await _repository.AddAsync(entity);

            if (!result.IsSuccess)
            {
                return Result<Educations>.Invalid(result.ValidationErrors);
            }
            return result;
        }

        public async Task<Result<Educations>> UpdateEducationsAsync(EducationsUpdateDto dto)
        {
            var result = await _repository.GetByIdAsync(dto.Id);
            if (!result.IsSuccess)
            {
                return Result<Educations>.Invalid(result.ValidationErrors);
            }

            _mapper.Map(dto, result.Value);
            return await _repository.UpdateAsync(result.Value);
        }

        public async Task<Result> DeleteEducationsAsync(int id)
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
