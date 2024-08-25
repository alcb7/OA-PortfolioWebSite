using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Persistance;
using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Persistance.Repositories;
using OA.PortfolioWebSite.Persistance.Services;
using OA.PortfolioWebSite.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Veritabaný baðlantý dizesini yapýlandýrýyoruz
string dataConnectionString = builder.Configuration.GetConnectionString("DataConnection");

// DbContext'i ekliyoruz
builder.Services.AddDbContext<DataAPIDbContext>(options =>
    options.UseSqlServer(dataConnectionString));

// Diðer servisleri ekliyoruz
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

builder.Services.AddScoped<ISendContactService, SendContactService>(provider =>
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

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Veritabaný baþlangýç iþlemleri


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
