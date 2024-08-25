using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.DTOs
{
    public class BlogPostsCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
        public int? AuthorId { get; set; }
    }
}
