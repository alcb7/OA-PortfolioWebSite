using FluentValidation;
using OA.PortfolioWebSite.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Validations
{
    public class ExperiencesValidator : AbstractValidator<ExperiencesCreateDto>
    {
        public ExperiencesValidator()
        {
            //RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            //RuleFor(x => x.Company).NotEmpty().MaximumLength(100);
            //RuleFor(x => x.StartDate).NotEmpty();
            //RuleFor(x => x.EndDate).NotEmpty();
            //RuleFor(x => x.Description).NotEmpty();
        }
    }
}
