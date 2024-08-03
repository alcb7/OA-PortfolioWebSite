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
        DbSet<Experiences> Experiences { get; set; }
        DbSet<AboutMe> Abouts { get; set; }
        DbSet <Blog> Blogs { get; set; }
        DbSet <Projects> Projects { get; set; }
        DbSet <Service> Services { get; set; }




    }
}
