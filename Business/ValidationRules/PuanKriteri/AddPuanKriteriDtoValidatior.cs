using Core.Utilities.Helpers;
using Entities.Dtos.PuanKriteri;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.PuanKriteri
{
    public class AddPuanKriteriDtoValidatior : AbstractValidator<AddPuanKriteriDto>
    {
        public AddPuanKriteriDtoValidatior()
        {
            RuleFor(x => x.KriterId)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Kriter Id"));
            RuleFor(x => x.KriterId).GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Kriter Id", 0));

            RuleFor(x => x.AlanId)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Alan Id"));
            RuleFor(x => x.AlanId).GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Alan Id", 0));

            RuleFor(x => x.PozisyonId)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Pozisyon Id"));
            RuleFor(x => x.PozisyonId).GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Pozisyon Id", 0));

            RuleFor(x => x.MinPuan)
                .GreaterThan(0)
                .When(x => x.MinPuan.HasValue)
                .WithMessage(ValidationMessageHelper.GreaterThanMessage("Min Puan", 0));

            RuleFor(x => x.MaxPuan)
                .GreaterThan(0)
                .When(x => x.MaxPuan.HasValue)
                .WithMessage(ValidationMessageHelper.GreaterThanMessage("Max Puan", 0));

            RuleFor(x => x.MaxPuan)
                .GreaterThan(x => x.MinPuan.Value)
                .When(x => x.MinPuan.HasValue && x.MaxPuan.HasValue)
                .WithMessage("Maximum puan alanı, minimum puandan büyük olmalı.");
        }
    }
}
