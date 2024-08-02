using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Domain.Entities;
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
        DbSet<Experience> Experiences { get; set; }
        DbSet<About> Abouts { get; set; }
        DbSet <Blog> Blogs { get; set; }
        DbSet <Project> Projects { get; set; }
        DbSet <Service> Services { get; set; }




    }
}
