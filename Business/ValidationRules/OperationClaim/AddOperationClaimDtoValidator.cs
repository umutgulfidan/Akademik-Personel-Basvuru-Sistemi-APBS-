using Core.Utilities.Helpers;
using Entities.Dtos.OperationClaim;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.OperationClaim
{
    public class AddOperationClaimDtoValidator : AbstractValidator<AddOperationClaimDto>
    {
        public AddOperationClaimDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Name"))
                .MinimumLength(2)
                .WithMessage(ValidationMessageHelper.MinLengthMessage("Name", 2));

        }

    }

}
