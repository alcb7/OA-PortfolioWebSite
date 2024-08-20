using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.DTOs
{
    public class EducationsCreateDto
    {
        public string Degree { get; set; }
        public string School { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserId { get; set; }
    }
}
