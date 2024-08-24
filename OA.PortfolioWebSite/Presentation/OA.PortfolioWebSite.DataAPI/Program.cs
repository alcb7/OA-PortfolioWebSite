using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Application;
using OA.PortfolioWebSite.DataAPI;
using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Persistance.Repositories;
using OA.PortfolioWebSite.Persistance.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Retrieve connection string from appsettings.json
string dataConnectionString = builder.Configuration.GetConnectionString("DataConnection");

// Set up database context with connection string
builder.Services.AddDbContext<DataAPIDbContext>(options =>
    options.UseSqlServer(dataConnectionString));

// Register application services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAboutMeRepository, AboutMeRepository>();
builder.Services.AddScoped<IAboutMeService, AboutMeService>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IBlogPostsRepository, BlogPostsRepository>();
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IBlogPostsService, BlogPostsService>();
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEducationService, EducationsService>();
builder.Services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
builder.Services.AddScoped<IPersonalInfoService, PersonalInfoService>();
builder.Services.AddScoped<IContactMessagesRepository, ContactMessagesRepository>();
builder.Services.AddScoped<IContactMessagesService, ContactMessagesService>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<ICommentsService, CommentsService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Build the service provider and initialize the database
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataDbContext = scope.ServiceProvider.GetRequiredService<DataAPIDbContext>();

    // Uncomment the line below if you want to delete and recreate the database
    // dataDbContext.Database.EnsureDeleted();

    dataDbContext.Database.EnsureCreated();
    SeedData.Initialize(dataDbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
