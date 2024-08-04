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
    public class AboutMeService : IAboutMeService
    {
        private readonly IRepository<AboutMe> _repository;

        public AboutMeService(IRepository<AboutMe> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AboutMe>> GetAllAboutMeAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AboutMe> GetAboutMeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAboutMeAsync(AboutMe aboutMe)
        {
            await _repository.AddAsync(aboutMe);
        }

        public async Task UpdateAboutMeAsync(AboutMe aboutMe)
        {
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
