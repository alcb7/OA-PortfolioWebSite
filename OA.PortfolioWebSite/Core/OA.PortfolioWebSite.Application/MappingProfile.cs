using AutoMapper;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AboutMe, AboutMeCreateDto>().ReverseMap();
            CreateMap<AboutMe, AboutMeUpdateDto>().ReverseMap();
            CreateMap<BlogPosts, BlogPostsCreateDto>().ReverseMap();
            CreateMap<BlogPosts, BlogPostsUpdateDto>().ReverseMap();
            CreateMap<ExperiencesCreateDto, BlogPosts>().ReverseMap();
            CreateMap<ExperiencesUpdateDto, BlogPosts>().ReverseMap();
            CreateMap<ProjectsUpdateDto, Projects>().ReverseMap();
            CreateMap<ProjectsCreateDto, Projects>().ReverseMap();
            CreateMap<EducationsCreateDto, Educations>().ReverseMap();
            CreateMap<EducationsUpdateDto, Educations>().ReverseMap();
            CreateMap<PersonalInfoCreateDto, PersonalInfo>().ReverseMap();
            CreateMap<PersonalInfoUpdateDto, PersonalInfo>().ReverseMap();
            CreateMap<ContactMessagesCreateDto, ContactMessages>().ReverseMap();
            CreateMap<ContactMessagesUpdateDto, ContactMessages>().ReverseMap();
            CreateMap<CommentsCreateDto, Comments>().ReverseMap();
            CreateMap<CommentsUpdateDto, Comments>().ReverseMap();

        }
    }
}
