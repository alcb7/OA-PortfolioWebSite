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
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _repository;
        private readonly IMapper _mapper;

        public ExperienceService(IExperienceRepository experienceRepository, IMapper mapper)
        {
            _repository = experienceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Experiences>> GetAllExperiencesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Experiences> GetExperienceByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddExperienceAsync(ExperiencesCreateDto experienceCreateDto)
        {
            var aboutMe = _mapper.Map<Experiences>(experienceCreateDto);
            await _repository.AddAsync(aboutMe);
        }

        public async Task UpdateExperienceAsync(ExperiencesUpdateDto ExperinceUpdateDto)
        {
            var experinece = await _repository.GetByIdAsync(ExperinceUpdateDto.Id);
            if (experinece == null)
            {
                throw new ArgumentException("Experience not found");
            }

            _mapper.Map(ExperinceUpdateDto, experinece);
            await _repository.UpdateAsync(experinece);
        }

        public async Task DeleteExperienceAsync(int id)
        {
            var aboutMe = await _repository.GetByIdAsync(id);
            if (aboutMe != null)
            {
                await _repository.DeleteAsync(aboutMe);
            }
        }
    }
}
