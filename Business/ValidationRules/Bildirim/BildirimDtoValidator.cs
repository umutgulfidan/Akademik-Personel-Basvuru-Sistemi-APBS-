using Core.Utilities.Helpers;
using Entities.Dtos.Bildirim;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Bildirim
{
    public class BildirimDtoValidator : AbstractValidator<BildirimDto>
    {
        public BildirimDtoValidator()
        {
            RuleFor(x => x.Baslik)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Başlık"))
                .MaximumLength(100).WithMessage(ValidationMessageHelper.MaxLengthMessage("Başlık", 100));

            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Açıklama"))
                .MaximumLength(500).WithMessage(ValidationMessageHelper.MaxLengthMessage("Başlık", 100));

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("İkon"))
                .MaximumLength(250).WithMessage(ValidationMessageHelper.MaxLengthMessage("İkon", 250));

            RuleFor(x => x.Renk)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Renk"))
                .MaximumLength(100).WithMessage(ValidationMessageHelper.MaxLengthMessage("Renk", 100));
        }
    }
}
