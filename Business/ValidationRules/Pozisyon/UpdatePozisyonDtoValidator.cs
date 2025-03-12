using Entities.Dtos.Pozisyon;
using FluentValidation;

namespace Business.ValidationRules.Pozisyon
{
    public class UpdatePozisyonDtoValidator : AbstractValidator<UpdatePozisyonDto>
    {
        public UpdatePozisyonDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID alanı boş geçilemez");

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Ad alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Ad alanı en az 2 karakter olmalıdır.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olmalıdır.");
        }
    }
}
