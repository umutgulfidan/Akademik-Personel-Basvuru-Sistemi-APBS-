using Core.Utilities.Helpers;
using Entities.Dtos.Kriter;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Kriter
{
    public class UpdateKriterDtoValidator : AbstractValidator<UpdateKriterDto>
    {
        public UpdateKriterDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Id"))
                .GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Id", 0));
            RuleFor(x => x.Ad)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Ad"));

            RuleFor(x => x.Ad)
                .MinimumLength(2)
                .WithMessage(ValidationMessageHelper.MinLengthMessage("Ad", 2));
        }
    }
}
