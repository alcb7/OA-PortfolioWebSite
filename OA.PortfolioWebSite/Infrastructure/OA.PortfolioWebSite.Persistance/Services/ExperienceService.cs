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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _repository;
        private readonly IMapper _mapper;

        public ExperienceService(IExperienceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<Experiences>>> GetAllExperiencesAsync()
        {
            var experienceEntities = await _repository.GetAllAsync();
            if (!experienceEntities.IsSuccess)
            {
                return Result<IEnumerable<Experiences>>.Invalid(experienceEntities.ValidationErrors);
            }
            return experienceEntities;
        }

        public async Task<Result<Experiences>> GetExperienceByIdAsync(int id)
        {
            var experienceEntity = await _repository.GetByIdAsync(id);
            if (!experienceEntity.IsSuccess)
            {
                return Result<Experiences>.Invalid(experienceEntity.ValidationErrors);
            }
            return experienceEntity;
        }

        public async Task<Result<Experiences>> AddExperienceAsync(ExperiencesCreateDto experienceCreateDto)
        {
            var experienceEntity = _mapper.Map<Experiences>(experienceCreateDto);
            var result = await _repository.AddAsync(experienceEntity);

            if (!result.IsSuccess)
            {
                return Result<Experiences>.Invalid(result.ValidationErrors);
            }
            return result;
        }

        public async Task<Result<Experiences>> UpdateExperienceAsync(ExperiencesUpdateDto experienceUpdateDto)
        {
            var experienceResult = await _repository.GetByIdAsync(experienceUpdateDto.Id);
            if (!experienceResult.IsSuccess)
            {
                return Result<Experiences>.Invalid(experienceResult.ValidationErrors);
            }

            _mapper.Map(experienceUpdateDto, experienceResult.Value);
            return await _repository.UpdateAsync(experienceResult.Value);
        }

        public async Task<Result> DeleteExperienceAsync(int id)
        {
            var experienceResult = await _repository.GetByIdAsync(id);
            if (!experienceResult.IsSuccess)
            {
                return Result.Invalid(experienceResult.ValidationErrors);
            }

            return await _repository.DeleteAsync(id);
        }
    }
}
