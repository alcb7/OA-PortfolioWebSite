using Ardalis.Result;
using AutoMapper;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Domain.Entities.Data;
using OA.PortfolioWebSite.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class AboutMeService : IAboutMeService
    {
        private readonly IAboutMeRepository _repository;
        private readonly IMapper _mapper;

        public AboutMeService(IAboutMeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AboutMe>>> GetAllAboutMeAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Result<AboutMe>> GetAboutMeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Result<AboutMe>> AddAboutMeAsync(AboutMeCreateDto aboutMeCreateDto)
        {
            var aboutMe = _mapper.Map<AboutMe>(aboutMeCreateDto);
            return await _repository.AddAsync(aboutMe);
        }

        public async Task<Result<AboutMe>> UpdateAboutMeAsync(AboutMeUpdateDto aboutMeUpdateDto)
        {
            var aboutMeResult = await _repository.GetByIdAsync(aboutMeUpdateDto.Id);
            if (!aboutMeResult.IsSuccess)
            {
                return Result<AboutMe>.NotFound();
            }

            var aboutMe = aboutMeResult.Value;
            _mapper.Map(aboutMeUpdateDto, aboutMe);
            return await _repository.UpdateAsync(aboutMe);
        }

        public async Task<Result> DeleteAboutMeAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
