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
    public class RegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public RegisterValidator()
        {
            // NOT EMPTY
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Kullanıcı adı"));
            RuleFor(x => x.LastName).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Kullanıcı soyadı"));
            RuleFor(x => x.NationalityId).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("TC No"));
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Eposta"));
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Doğum günü"));
            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Şifre"));
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Şifre tekrarı"));

            // MIN - MAX LENGTHS OR VALUES
            RuleFor(x => x.NationalityId).Length(11).WithMessage(ValidationMessageHelper.ExactLengthMessage("TC No", 11));
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage(ValidationMessageHelper.MinLengthMessage("Kullanıcı adı", 2));
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage(ValidationMessageHelper.MinLengthMessage("Kullanıcı soyadı", 2));
            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now).WithMessage("Doğum günü kabul edilen aralık dışında.");
            RuleFor(x => x.DateOfBirth).GreaterThanOrEqualTo(new DateTime(1900, 1, 1)).WithMessage("Doğum günü kabul edilen aralık dışında.");

            // PASSWORD RULES
            RuleFor(x => x.Password).MinimumLength(6).WithMessage(ValidationMessageHelper.MinLengthMessage("Şifre", 6));
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsLowerCase).WithMessage("Şifre en az bir adet küçük harf içermelidir.");
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsUpperCase).WithMessage("Şifre en az bir adet büyük harf içermelidir.");
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsDigit).WithMessage("Şifre en az bir adet rakam içermelidir.");
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsAllowedSpecialCharacter).WithMessage("Şifre en az bir adet özel karakter içermelidir.");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Şifreler uyuşmuyor.");

            // OTHERS
            RuleFor(x => x.NationalityId).Must(CustomValidatorRules.StartsWithNonZero).WithMessage("TC No '0' karakteri ile başlayamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage(ValidationMessageHelper.EmailMessage("Eposta"));
        }
    }
}
