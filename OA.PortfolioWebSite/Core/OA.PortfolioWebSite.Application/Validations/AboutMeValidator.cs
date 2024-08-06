using FluentValidation;
using OA.PortfolioWebSite.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Validations
{
    public class AboutMeValidator : AbstractValidator<AboutMeCreateDto>
    {
        public AboutMeValidator()
        {
            RuleFor(x => x.Introduction).NotEmpty();
            RuleFor(x => x.ImageUrl1).MaximumLength(255);
            RuleFor(x => x.ImageUrl2).MaximumLength(255);
        }
    }
}
