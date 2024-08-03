using OA.PortfolioWebSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Repositories
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
