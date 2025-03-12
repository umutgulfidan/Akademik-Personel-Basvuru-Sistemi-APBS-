using Entities.Dtos.OperationClaim;
using FluentValidation;

namespace Business.ValidationRules.OperationClaim
{
    public class UpdateOperationClaimDtoValidator : AbstractValidator<UpdateOperationClaimDto>
    {
        public UpdateOperationClaimDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id alanı boş geçilemez").GreaterThan(0).WithMessage("ID alanı 0 dan büyük olmalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Name alanı minimum 2 karakterden oluşmalıdır.");
        }
    }

}
