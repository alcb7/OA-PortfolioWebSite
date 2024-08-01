using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Contexts
{
    public class AuthAPIDbContext : DbContext
    {
        public AuthAPIDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
