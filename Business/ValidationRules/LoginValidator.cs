
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class LoginValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x=> x.NationalityId).NotEmpty().WithMessage("TC No alanı boş geçilemez");
            RuleFor(x=> x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez");
            RuleFor(x => x.NationalityId).Length(11).WithMessage("TC No alanı doğru uzunlukta değil.");
            RuleFor(x => x.NationalityId).Must(CustomValidatorRules.StartsWithNonZero).WithMessage("TC No '0' karakteri ile başlayamaz.");
        }

    }
}
