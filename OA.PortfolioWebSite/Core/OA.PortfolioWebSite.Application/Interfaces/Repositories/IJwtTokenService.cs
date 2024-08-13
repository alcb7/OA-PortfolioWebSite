using OA.PortfolioWebSite.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Repositories
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
