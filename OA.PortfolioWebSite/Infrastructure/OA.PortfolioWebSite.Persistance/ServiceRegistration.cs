using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Application.Repositories;
using OA.PortfolioWebSite.Persistance.Services;

namespace OA.PortfolioWebSite.Persistance
{
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
            }
        }
    }
}
