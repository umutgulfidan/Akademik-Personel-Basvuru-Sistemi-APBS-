using Core.Utilities.Helpers;
using Entities.Dtos.AlanKriteri;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.AlanKriteri
{
    public class UpdateAlanKriteriDtoValidator : AbstractValidator<UpdateAlanKriteriDto>
    {
        public UpdateAlanKriteriDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessageHelper.RequiredMessage("Id"));
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Id", 0));

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

            RuleFor(x => x.MinAdet).GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Min Adet", 0));
        }
    }
}
