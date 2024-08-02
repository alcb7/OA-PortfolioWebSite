using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Persistance.Contexts;

namespace OA.PortfolioWebSite.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(IServiceCollection services)
        {
            const string authConnectionString = "Server=.;Database=AuthDb;User Id=publish_user;Password=123456;TrustServerCertificate=True;";
            const string dataConnectionString = "Server=.;Database=DataDb;User Id=publish_user;Password=123456;TrustServerCertificate=True;";
            services.AddDbContext<AuthAPIDbContext>(options =>
                options.UseSqlServer(authConnectionString));

            services.AddDbContext<DataAPIDbContext>(options =>
                options.UseSqlServer(dataConnectionString));

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
