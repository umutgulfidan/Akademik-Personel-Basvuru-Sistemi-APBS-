using Entities.Dtos.UserOperationClaim;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.UserOperationClaim
{
    public class UpdateUserOperationClaimDtoValidator : AbstractValidator<UpdateUserOperationClaimDto>
    {
        public UpdateUserOperationClaimDtoValidator()
        {
            RuleFor(x=> x.Id).NotEmpty().WithMessage("Id alanı boş olamaz.")
                .GreaterThan(0).WithMessage("Id alanı 0'dan büyük olmalıdır.");
            RuleFor(x => x.OperationClaimId).NotEmpty().WithMessage("OperationClaimId alanı boş geçilemez.")
                .GreaterThan(0).WithMessage("OperationClaimId alanı 0'dan büyük olmalıdır.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId alanı boş geçilemez")
                .GreaterThan(0).WithMessage("UserId alanı 0 dan büyük olmalıdır.");
        }
    }
}
