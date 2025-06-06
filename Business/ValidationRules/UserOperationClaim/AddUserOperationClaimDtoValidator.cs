﻿using Core.Utilities.Helpers;
using Entities.Dtos.UserOperationClaim;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.UserOperationClaim
{
    public class AddUserOperationClaimDtoValidator : AbstractValidator<AddUserOperationClaimDto>
    {
        public AddUserOperationClaimDtoValidator()
        {
            RuleFor(x => x.OperationClaimId)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("OperationClaimId"))
                .GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("OperationClaimId", 0));

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("UserId"))
                .GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("UserId", 0));
        }
    }
}
