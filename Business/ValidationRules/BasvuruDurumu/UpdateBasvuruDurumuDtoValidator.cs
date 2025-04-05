using Core.Utilities.Helpers;
using Entities.Dtos.BasvuruDurumu;
using FluentValidation;

namespace Business.ValidationRules.BasvuruDurumu
{
    public class UpdateBasvuruDurumuDtoValidator : AbstractValidator<UpdateBasvuruDurumuDto>
    {
        public UpdateBasvuruDurumuDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Id"));

            RuleFor(x => x.Ad).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Ad"));
            RuleFor(x => x.Ad).MinimumLength(2).WithMessage(ValidationMessageHelper.MinLengthMessage("Ad", 2));
            RuleFor(x => x.Ad).MaximumLength(200).WithMessage(ValidationMessageHelper.MaxLengthMessage("Ad", 200));
        }
    }
}
