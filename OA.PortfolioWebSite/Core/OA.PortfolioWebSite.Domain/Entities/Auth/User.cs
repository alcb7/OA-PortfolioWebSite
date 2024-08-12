using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.PortfolioWebSite.Domain.Entities.Common;
using OA.PortfolioWebSite.Domain.Entities.Data;

namespace OA.PortfolioWebSite.Domain.Entities.Auth
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
       public ICollection<Experiences> Experiences { get; set; }
       public ICollection<Educations> Educations { get; set; }
     public ICollection<Projects> Projects { get; set; }
     public ICollection<BlogPosts> BlogPosts { get; set; }
     public ICollection<Comments> Comments { get; set; }

    }
}
