﻿using Core.Utilities.Helpers;
using Entities.Dtos.Bolum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Bolum
{
    public class UpdateBolumDtoValidator : AbstractValidator<UpdateBolumDto>
    {
        public UpdateBolumDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Id"))
                .GreaterThan(0)
                .WithMessage(ValidationMessageHelper.GreaterThanMessage("Id",0));

            RuleFor(x => x.AlanId)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("AlanId"))
                .GreaterThan(0)
                .WithMessage(ValidationMessageHelper.GreaterThanMessage("AlanId", 0));

            RuleFor(x => x.Ad)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Ad"))
                .MinimumLength(2)
                .WithMessage(ValidationMessageHelper.MinLengthMessage("Ad", 2));

            RuleFor(x => x.Aciklama)
                .MaximumLength(500)
                .WithMessage(ValidationMessageHelper.MaxLengthMessage("Açıklama", 500));

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(ValidationMessageHelper.EmailMessage("Email"));

            RuleFor(x => x.Adres)
                .MaximumLength(250)
                .WithMessage(ValidationMessageHelper.MaxLengthMessage("Adres", 250));
        }
    }
}
