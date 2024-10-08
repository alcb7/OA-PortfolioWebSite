﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Domain.Entities.Common;
using OA.PortfolioWebSite.Domain.Entities.Data;

namespace OA.PortfolioWebSite.Domain.Entities
{
    public class BlogPosts : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }

        public int? AuthorId { get; set; }
      // public ICollection<Comments>? Comments { get; set; }

    }
}
