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
            RuleFor(x=> x.Id).NotEmpty().WithMessage("Id alanı boş geçilemez.");

            RuleFor(x => x.AlanId)
                .GreaterThan(0).WithMessage("AlanId pozitif bir sayı olmalıdır.");

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Ad alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Ad alanı en az 2 karakter olmalıdır.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olmalıdır.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girilmelidir.");

            RuleFor(x => x.Adres)
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olmalıdır.");
        }
    }
}
