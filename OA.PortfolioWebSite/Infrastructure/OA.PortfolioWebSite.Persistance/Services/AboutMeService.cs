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
        private readonly IRepository<AboutMe> _repository;
        private readonly IMapper _mapper;

        public AboutMeService(IRepository<AboutMe> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AboutMe>> GetAllAboutMeAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AboutMe> GetAboutMeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAboutMeAsync(AboutMeCreateDto aboutMeCreateDto)
        {
            var aboutMe = _mapper.Map<AboutMe>(aboutMeCreateDto);
            await _repository.AddAsync(aboutMe);
        }

        public async Task UpdateAboutMeAsync(AboutMeUpdateDto aboutMeUpdateDto)
        {
            var aboutMe = await _repository.GetByIdAsync(aboutMeUpdateDto.Id);
            if (aboutMe == null)
            {
                throw new ArgumentException("AboutMe not found");
            }

            _mapper.Map(aboutMeUpdateDto, aboutMe);
            await _repository.UpdateAsync(aboutMe);
        }

        public async Task DeleteAboutMeAsync(int id)
        {
            var aboutMe = await _repository.GetByIdAsync(id);
            if (aboutMe != null)
            {
                await _repository.DeleteAsync(aboutMe);
            }
        }
    }
}
