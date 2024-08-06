using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Domain.Entities.Common;

namespace OA.PortfolioWebSite.Domain.Entities.Data
{
    public class Projects : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
