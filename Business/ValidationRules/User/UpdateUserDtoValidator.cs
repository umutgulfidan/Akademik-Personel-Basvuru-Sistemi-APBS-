using Core.Utilities.Helpers;
using Entities.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.User
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Eposta"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(ValidationMessageHelper.EmailMessage("Eposta"));
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Mevcut Şifre"));
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Yeni Şifre"));
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Yeni Şifre tekrarı"));

            // PASSWORD RULES
            RuleFor(x => x.NewPassword).MinimumLength(6).WithMessage(ValidationMessageHelper.MinLengthMessage("Şifre", 6));
            RuleFor(x => x.NewPassword).Must(CustomValidatorRules.ContainsLowerCase).WithMessage("Şifre en az bir adet küçük harf içermelidir.");
            RuleFor(x => x.NewPassword).Must(CustomValidatorRules.ContainsUpperCase).WithMessage("Şifre en az bir adet büyük harf içermelidir.");
            RuleFor(x => x.NewPassword).Must(CustomValidatorRules.ContainsDigit).WithMessage("Şifre en az bir adet rakam içermelidir.");
            RuleFor(x => x.NewPassword).Must(CustomValidatorRules.ContainsAllowedSpecialCharacter).WithMessage("Şifre en az bir adet özel karakter içermelidir.");
            RuleFor(x => x.NewPassword).Equal(x => x.ConfirmNewPassword).WithMessage("Şifreler uyuşmuyor.");

        }
    }
}
