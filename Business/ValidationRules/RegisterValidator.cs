using Business.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class RegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public RegisterValidator() 
        {
            // NOT EMPTY
            RuleFor(x=> x.FirstName).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Kullanıcı soyadı boş geçilemez.");
            RuleFor(x => x.NationalityId).NotEmpty().WithMessage("TC No boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Eposta boş geçilemez.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Doğum günü boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrarı boş geçilemez.");
            
            // MIN - MAX LENGTHS OR VALUES
            RuleFor(x => x.NationalityId).Length(11).WithMessage("TC No alanı doğru uzunlukta değil.");
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage("Kullanıcı adı 2 karakterden kısa olamaz");
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage("Kullanıcı soyadı 2 karakterden kısa olamaz");
            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now).WithMessage("Doğum günü kabul edilen aralık dışında.");
            RuleFor(x=> x.DateOfBirth).GreaterThanOrEqualTo(new DateTime(1900,1,1)).WithMessage("Doğum günü kabul edilen aralık dışında.");


            // PASSWORD RULES
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre en az 2 karakter olmalıdır.");
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsLowerCase).WithMessage("Şifre en az bir adet küçük harf içermelidir.");
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsUpperCase).WithMessage("Şifre en az bir adet büyük harf içermelidir.");
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsDigit).WithMessage("Şifre en az bir adet rakam içermelidir.");
            RuleFor(x => x.Password).Must(CustomValidatorRules.ContainsAllowedSpecialCharacter).WithMessage("Şifre en az bir adet özel karakter içermelidir.");
            RuleFor(x=> x.Password).Equal(x=> x.ConfirmPassword).WithMessage("Şifreler uyuşmuyor.");

            // OTHERS
            RuleFor(x => x.NationalityId).Must(CustomValidatorRules.StartsWithNonZero).WithMessage("TC No '0' karakteri ile başlayamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email formatı doğru değil.");
        }
    }
}
