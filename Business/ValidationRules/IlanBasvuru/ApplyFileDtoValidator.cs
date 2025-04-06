using Core.Utilities.Helpers;
using Entities.Dtos.IlanBasvuru;
using FluentValidation;

namespace Business.ValidationRules.IlanBasvuru
{
    public class ApplyFileDtoValidator : AbstractValidator<ApplyFileDto>
    {
        public ApplyFileDtoValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage(ValidationMessageHelper.RequiredMessage("Dosya"))
                .Must(file => file.Length > 0).WithMessage("Boş dosya gönderilemez.");

            RuleFor(x => x.KriterIds)
                .NotNull().WithMessage(ValidationMessageHelper.RequiredMessage("Kriter Id"))
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Kriter Id"));

            RuleForEach(x => x.KriterIds)
                .GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Kriter Id",0));
        }
    }
}
