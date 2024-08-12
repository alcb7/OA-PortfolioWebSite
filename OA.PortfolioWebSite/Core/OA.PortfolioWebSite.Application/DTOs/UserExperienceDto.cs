using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.DTOs
{
    public class UserExperienceDto
    {
        public User User { get; set; }
        public IEnumerable<Experiences> Experiences { get; set; }
    }
}
