using Microsoft.Extensions.DependencyInjection;
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

public static class ServiceRegistration
{
    public static void AddPersistenceServices(IServiceCollection services)
    {
        ConfigurationManager configurationAuth = new();
        configurationAuth.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OA.PortfolioWebSite.AuthAPI"));
        configurationAuth.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        ConfigurationManager configurationData = new();
        configurationData.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OA.PortfolioWebSite.DataAPI"));
        configurationData.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        string authConnectionString = configurationAuth.GetConnectionString("AuthConnection");
        string dataConnectionString = configurationData.GetConnectionString("DataConnection");

        services.AddDbContext<AuthAPIDbContext>(options =>
            options.UseSqlServer(authConnectionString));
        services.AddDbContext<DataAPIDbContext>(options =>
            options.UseSqlServer(dataConnectionString));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IAboutMeRepository, AboutMeRepository>();
        services.AddScoped<IAboutMeService, AboutMeService>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();
        services.AddScoped<IExperienceService, ExperienceService>();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddValidatorsFromAssemblyContaining<AboutMeValidator>();
        services.AddValidatorsFromAssemblyContaining<ExperiencesValidator>();

        services.AddMvc().AddFluentValidation();

        var serviceProvider = services.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var authDbContext = scope.ServiceProvider.GetRequiredService<AuthAPIDbContext>();
            var dataDbContext = scope.ServiceProvider.GetRequiredService<DataAPIDbContext>();

            // Veritabanını sil ve yeniden oluştur
            authDbContext.Database.EnsureDeleted();
            authDbContext.Database.EnsureCreated();

            dataDbContext.Database.EnsureDeleted();
            dataDbContext.Database.EnsureCreated();

            //SeedData.Initialize(authDbContext, dataDbContext);
        }
    }
}
