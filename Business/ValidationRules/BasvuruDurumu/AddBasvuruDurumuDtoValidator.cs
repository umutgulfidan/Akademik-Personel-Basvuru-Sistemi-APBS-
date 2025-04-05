using Core.Utilities.Helpers;
using Entities.Dtos.BasvuruDurumu;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.BasvuruDurumu
{
    public class AddBasvuruDurumuDtoValidator : AbstractValidator<AddBasvuruDurumuDto>
    {
        public AddBasvuruDurumuDtoValidator()
        {
            RuleFor(x => x.Ad).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Ad"));
            RuleFor(x => x.Ad).MinimumLength(2).WithMessage(ValidationMessageHelper.MinLengthMessage("Ad",2));
            RuleFor(x => x.Ad).MaximumLength(200).WithMessage(ValidationMessageHelper.MaxLengthMessage("Ad",200));
        }
    }
}
