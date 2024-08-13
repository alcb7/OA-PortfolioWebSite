using Microsoft.Extensions.DependencyInjection;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Application.Validations;
using OA.PortfolioWebSite.Application;
using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Persistance.Repositories;
using OA.PortfolioWebSite.Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace OA.PortfolioWebSite.Persistance
{
    public static class ServiceRegistration2
    {
        public static void AddPersistenceServices2(IServiceCollection services)
        {
            ConfigurationManager configurationAuth = new();
            configurationAuth.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OA.PortfolioWebSite.AuthAPI"));
            configurationAuth.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            

            string authConnectionString = configurationAuth.GetConnectionString("AuthConnection");

            services.AddDbContext<AuthAPIDbContext>(options =>
                options.UseSqlServer(authConnectionString));
           

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

           


            // services.AddScoped<IValidator<AboutMeCreateDto>, AboutMeValidator>();

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var authDbContext = scope.ServiceProvider.GetRequiredService<AuthAPIDbContext>();

                // Veritabanını sil ve yeniden oluştur
                authDbContext.Database.EnsureDeleted();
                authDbContext.Database.EnsureCreated();

              

               // SeedData.Initialize(authDbContext, dataDbContext);
            }
        }
    }
}
