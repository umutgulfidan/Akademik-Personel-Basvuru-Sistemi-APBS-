using Core.Utilities.Helpers;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Auth
{
    public class LoginValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.NationalityId)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("TC No"))
                .Length(11).WithMessage(ValidationMessageHelper.ExactLengthMessage("TC No", 11))
                .Must(CustomValidatorRules.StartsWithNonZero).WithMessage("TC No '0' karakteri ile başlayamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Şifre"));
        }

    }
}
