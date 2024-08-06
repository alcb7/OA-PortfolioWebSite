using AutoMapper;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OA.PortfolioWebSite.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AboutMe, AboutMeCreateDto>().ReverseMap();
            CreateMap<AboutMe, AboutMeUpdateDto>().ReverseMap();
        }
    }
}
