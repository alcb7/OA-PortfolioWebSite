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
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IPersonalInfoRepository _repository;
        private readonly IMapper _mapper;

        public PersonalInfoService(IPersonalInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<PersonalInfo>>> GetAllPersonalInfoAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (!entities.IsSuccess)
            {
                return Result<IEnumerable<PersonalInfo>>.Invalid(entities.ValidationErrors);
            }
            return entities;
        }
        

        public async Task<Result<PersonalInfo>> GetPersonalInfoByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (!entity.IsSuccess)
            {
                return Result<PersonalInfo>.Invalid(entity.ValidationErrors);
            }
            return entity;
        }

        public async Task<Result<PersonalInfo>> AddPersonalInfoAsync(PersonalInfoCreateDto dto)
        {
            var entity = _mapper.Map<PersonalInfo>(dto);
            var result = await _repository.AddAsync(entity);

            if (!result.IsSuccess)
            {
                return Result<PersonalInfo>.Invalid(result.ValidationErrors);
            }
            return result;
        }

        public async Task<Result<PersonalInfo>> UpdatePersonalInfoAsync(PersonalInfoUpdateDto dto)
        {
            var result = await _repository.GetByIdAsync(dto.Id);
            if (!result.IsSuccess)
            {
                return Result<PersonalInfo>.Invalid(result.ValidationErrors);
            }

            _mapper.Map(dto, result.Value);
            return await _repository.UpdateAsync(result.Value);
        }

        public async Task<Result> DeletePersonalInfoAsync(int id)
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
