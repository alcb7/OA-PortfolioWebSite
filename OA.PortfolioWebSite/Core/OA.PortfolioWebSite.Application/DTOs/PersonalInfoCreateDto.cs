﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.DTOs
{
    public class PersonalInfoCreateDto
    {
        public string About { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public string MyProperty { get; set; }
    }
}

