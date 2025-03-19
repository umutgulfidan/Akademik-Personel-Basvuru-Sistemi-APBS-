using Core.Utilities.Helpers;
using Entities.Dtos.Pozisyon;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Pozisyon
{
    public class AddPozisyonDtoValidator : AbstractValidator<AddPozisyonDto>
    {
        public AddPozisyonDtoValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Ad"))
                .MinimumLength(2).WithMessage(ValidationMessageHelper.MinLengthMessage("Ad", 2));

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage(ValidationMessageHelper.MaxLengthMessage("Açıklama", 500));

        }
    }
}
