using OA.PortfolioWebSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.DTOs
{
    public class CommentsUpdateDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public int BlogPostId { get; set; }
        public int UserId { get; set; }
    }
}
