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
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Name alanı minimum 2 karakterden oluşmalıdır.");

        }

    }

}
