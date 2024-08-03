using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.PortfolioWebSite.Domain.Entities.Common;

namespace OA.PortfolioWebSite.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }

    }
}
