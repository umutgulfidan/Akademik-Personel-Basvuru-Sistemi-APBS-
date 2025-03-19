using Core.Utilities.Helpers;
using Entities.Dtos.OperationClaim;
using FluentValidation;

namespace Business.ValidationRules.OperationClaim
{
    public class UpdateOperationClaimDtoValidator : AbstractValidator<UpdateOperationClaimDto>
    {
        public UpdateOperationClaimDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Id"))
                .GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Id", 0));

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Name"))
                .MinimumLength(2).WithMessage(ValidationMessageHelper.MinLengthMessage("Name", 2));
        }
    }

}
