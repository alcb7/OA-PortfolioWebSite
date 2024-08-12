using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Domain.Entities.Data
{
    public class Comments : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public int BlogPostId { get; set; }
        public BlogPosts BlogPost { get; set; }
         public int UserId { get; set; }
    }
}
