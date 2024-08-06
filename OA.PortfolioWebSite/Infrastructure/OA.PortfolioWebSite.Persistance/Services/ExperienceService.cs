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
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;

        public ExperienceService(IExperienceRepository experienceRepository, IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExperienceDto>> GetAllExperiencesAsync()
        {
            var experiences = await _experienceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExperienceDto>>(experiences);
        }

        public async Task<ExperienceDto> GetExperienceByIdAsync(int id)
        {
            var experience = await _experienceRepository.GetByIdAsync(id);
            return _mapper.Map<ExperienceDto>(experience);
        }

        public async Task<ExperienceDto> AddExperienceAsync(ExperiencesCreateDto experienceCreateDto)
        {
            var experience = _mapper.Map<Experiences>(experienceCreateDto);
            await _experienceRepository.AddAsync(experience);
            return _mapper.Map<ExperienceDto>(experience);
        }

        public async Task<ExperienceDto> UpdateExperienceAsync(int id, ExperiencesUpdateDto experienceUpdateDto)
        {
            if (id != experienceUpdateDto.Id)
            {
                throw new ArgumentException("ID in the URL does not match ID in the body.");
            }

            var experience = await _experienceRepository.GetByIdAsync(id);
            if (experience == null)
            {
                throw new ArgumentException("Experience not found.");
            }

            _mapper.Map(experienceUpdateDto, experience);
            await _experienceRepository.UpdateAsync(experience);

            return _mapper.Map<ExperienceDto>(experience);
        }

        public async Task DeleteExperienceAsync(int id)
        {
            var experience = await _experienceRepository.GetByIdAsync(id);
            if (experience == null)
            {
                throw new ArgumentException("Experience not found.");
            }

            await _experienceRepository.DeleteAsync(experience);
        }
    }
}
