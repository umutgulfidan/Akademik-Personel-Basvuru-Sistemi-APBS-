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
    public class UpdateUserInfoDtoValidator : AbstractValidator<UpdateUserInfoDto>
    {
        public UpdateUserInfoDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Eposta"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(ValidationMessageHelper.EmailMessage("Eposta"));
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Mevcut Şifre"));

        }
    }
}
