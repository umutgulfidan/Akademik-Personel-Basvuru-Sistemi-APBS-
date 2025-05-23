﻿using Core.Utilities.Helpers;
using Entities.Dtos.IlanBasvuru;
using FluentValidation;

namespace Business.ValidationRules.IlanBasvuru
{
    public class ApplyDtoValidator : AbstractValidator<ApplyDto>
    {
        public ApplyDtoValidator()
        {
            RuleFor(x => x.IlanId)
                .GreaterThan(0).WithMessage(ValidationMessageHelper.RequiredMessage("İlan Id"));

            RuleFor(x => x.Files)
                .NotNull().WithMessage("Dosyalar boş olamaz.")
                .NotEmpty().WithMessage("En az bir dosya yüklenmelidir.");

            RuleForEach(x => x.Files).SetValidator(new ApplyFileDtoValidator());
        }
    }
}
