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
            //modelBuilder.ApplyConfiguration(new BlogPostConfiguration());

            modelBuilder.Entity<Experiences>()
        .HasOne(e => e.User)
        .WithMany(u => u.Experiences)
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Restrict);

            // User - Education
            modelBuilder.Entity<Educations>()
                .HasOne(e => e.User)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Project
            modelBuilder.Entity<Projects>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // BlogPost - Comments
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.BlogPost)
                .WithMany(bp => bp.Comments)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Comments
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull); // Foreign key null olabilir

            // BlogPost - User (Author)
            modelBuilder.Entity<BlogPosts>()
                .HasOne(bp => bp.Author)
                .WithMany(u => u.BlogPosts)
                .HasForeignKey(bp => bp.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);




        }
        public DbSet<Experiences> Experiences { get; set; }
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
