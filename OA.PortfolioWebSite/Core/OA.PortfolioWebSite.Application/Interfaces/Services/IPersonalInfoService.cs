using Ardalis.Result;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface IPersonalInfoService
    {
        Task<Result<IEnumerable<PersonalInfo>>> GetAllPersonalInfoAsync();
        Task<Result<PersonalInfo>> GetPersonalInfoByIdAsync(int id);
        Task<Result<PersonalInfo>> AddPersonalInfoAsync(PersonalInfoCreateDto dto);
        Task<Result<PersonalInfo>> UpdatePersonalInfoAsync(PersonalInfoUpdateDto dto);
        Task<Result> DeletePersonalInfoAsync(int id);

    }
}
