using Core.Utilities.Helpers;
using Entities.Dtos.Pozisyon;
using FluentValidation;

namespace Business.ValidationRules.Pozisyon
{
    public class UpdatePozisyonDtoValidator : AbstractValidator<UpdatePozisyonDto>
    {
        public UpdatePozisyonDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Id"));

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Ad"))
                .MinimumLength(2).WithMessage(ValidationMessageHelper.MinLengthMessage("Ad", 2));

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage(ValidationMessageHelper.MaxLengthMessage("Açıklama", 500));
        }
    }
}
