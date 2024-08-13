using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Domain.Entities;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Contexts
{
    public class DataAPIDbContext : DbContext
    {
        public DataAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AboutMeConfiguration());
            //modelBuilder.ApplyConfiguration(new ExperienceConfiguration());
            //modelBuilder.ApplyConfiguration(new CommentsConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
        public DbSet<BlogPosts> Experiences { get; set; }
       public DbSet<AboutMe> Abouts { get; set; }
       public DbSet <BlogPosts> BlogPosts { get; set; }
       public DbSet <Projects> Projects { get; set; }
       public DbSet <Service> Services { get; set; }
        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<ContactMessages> ContactMessages { get; set; }
        public DbSet<Comments> Comments { get; set; }





    }
}
