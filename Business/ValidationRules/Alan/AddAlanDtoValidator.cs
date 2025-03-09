using Business.Dtos.Alan;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Alan
{
    public class AddAlanDtoValidator : AbstractValidator<AddAlanDto>
    {
        public AddAlanDtoValidator()
        {
            RuleFor(x=> x.Ad).NotEmpty().WithMessage("Ad Alanı Boş Geçilemez");
            RuleFor(x => x.Ad).MinimumLength(2).WithMessage("Ad Alanı minimum 2 karakterden oluşmali.");
        }
    }
}
