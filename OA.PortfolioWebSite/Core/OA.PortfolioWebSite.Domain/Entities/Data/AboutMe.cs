using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.PortfolioWebSite.Domain.Entities.Common;

namespace OA.PortfolioWebSite.Domain.Entities.Data
{
    public class AboutMe : BaseEntity
    {
        public string Introduction { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
    }
}
