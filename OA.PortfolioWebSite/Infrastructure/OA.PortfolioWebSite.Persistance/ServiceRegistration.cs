﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using OA.PortfolioWebSite.Persistance.Contexts;
using FluentValidation.AspNetCore;
using FluentValidation;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Application.Validations;
using OA.PortfolioWebSite.Application;
using OA.PortfolioWebSite.Persistance.Repositories;
using OA.PortfolioWebSite.Persistance.Services;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities.Data;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(IServiceCollection services)
    {

        ConfigurationManager configurationData = new();
        configurationData.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OA.PortfolioWebSite.DataAPI"));
        configurationData.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        string dataConnectionString = configurationData.GetConnectionString("DataConnection");


        services.AddDbContext<DataAPIDbContext>(options =>
            options.UseSqlServer(dataConnectionString));


        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFileService, FileService>();


        services.AddScoped<IAboutMeRepository, AboutMeRepository>();
        services.AddScoped<IAboutMeService, AboutMeService>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IBlogPostsRepository, BlogPostsRepository>();
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<IBlogPostsService, BlogPostsService>();
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<IEducationService, EducationsService>();
        services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IContactMessagesRepository, ContactMessagesRepository>();
        services.AddScoped<IContactMessagesService, ContactMessagesService>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<ICommentsService, CommentsService>();
       // services.AddScoped<ISendContactService, SendContactService>();
        services.AddScoped<ISendContactService, SendContactService>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new SendContactService(
                provider.GetRequiredService<DataAPIDbContext>(),
                configuration["Smtp:Server"],             // string smtpServer
                int.Parse(configuration["Smtp:Port"]),    // int smtpPort
                configuration["Smtp:Username"],           // string smtpUsername
                configuration["Smtp:Password"],           // string smtpPassword
                configuration["Smtp:ReceiverEmail"]       // string receiverEmail
            );
        });



        services.AddAutoMapper(typeof(MappingProfile));

       // services.AddValidatorsFromAssemblyContaining<AboutMeValidator>();

        // services.AddScoped<IValidator<AboutMeCreateDto>, AboutMeValidator>();

        var serviceProvider = services.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var dataDbContext = scope.ServiceProvider.GetRequiredService<DataAPIDbContext>();

            // Veritabanını sil ve yeniden oluştur
            dataDbContext.Database.EnsureDeleted();

            dataDbContext.Database.EnsureCreated();
            SeedData.Initialize(dataDbContext);

        }
    }
}
