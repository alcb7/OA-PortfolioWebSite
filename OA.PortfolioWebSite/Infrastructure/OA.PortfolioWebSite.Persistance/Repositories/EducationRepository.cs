using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Domain.Entities.Data;
using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Repositories
{
    public class EducationRepository : Repository<Educations>, IEducationRepository
    {
        public EducationRepository(DataAPIDbContext context) : base(context)
        {
        }
    }
}
