using Entities.Dtos.Alan;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Alan
{
    public class UpdateAlanDtoValidator : AbstractValidator<UpdateAlanDto>
    {
        public UpdateAlanDtoValidator()
        {
            RuleFor(x=> x.Id).NotEmpty();
            RuleFor(x => x.Ad).NotEmpty().WithMessage("Ad Alanı Boş Geçilemez");
            RuleFor(x => x.Ad).MinimumLength(2).WithMessage("Ad Alanı minimum 2 karakterden oluşmali.");
        }
    }
}
