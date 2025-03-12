using Entities.Dtos.Pozisyon;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Pozisyon
{
    public class AddPozisyonDtoValidator : AbstractValidator<AddPozisyonDto>
    {
        public AddPozisyonDtoValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Ad alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Ad alanı en az 2 karakter olmalıdır.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olmalıdır.");
        }
    }
}
