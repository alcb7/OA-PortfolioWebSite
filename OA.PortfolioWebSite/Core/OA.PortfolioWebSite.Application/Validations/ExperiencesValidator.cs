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
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Company).NotEmpty().WithMessage("Company is required.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");

        }
    }
}
