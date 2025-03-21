using Core.Utilities.Helpers;
using Entities.Dtos.Ilan;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.Ilan
{
    public class AddIlanDtoValidator : AbstractValidator<AddIlanDto>
    {
        public AddIlanDtoValidator()
        {

            // PozisyonId zorunlu ve geçerli bir ID olmalı
            RuleFor(x => x.PozisyonId)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Pozisyon Id"))
                .GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Pozisyon Id",0));

            // BolumId zorunlu ve geçerli bir ID olmalı
            RuleFor(x => x.BolumId)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Bölüm Id"))
                .GreaterThan(0).WithMessage(ValidationMessageHelper.GreaterThanMessage("Bölüm Id", 0));

            // Baslik zorunlu ve 3 ile 100 karakter arasında olmalı
            RuleFor(x => x.Baslik)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Başlık"))
                .Length(3, 100).WithMessage(ValidationMessageHelper.RangeMessage("Başlık",3,100));

            // Aciklama zorunlu ve 10 ile 1000 karakter arasında olmalı
            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Açıklama"))
                .Length(10, 1000).WithMessage(ValidationMessageHelper.RangeMessage("Açıklama", 10, 1000));

            // BaslangicTarihi ve BitisTarihi geçerli bir tarihe sahip olmalı
            RuleFor(x => x.BaslangicTarihi)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Başlangıç Tarihi"))
                .GreaterThan(DateTime.Now).WithMessage("Başlangıç tarihi gelecek bir tarih olmalı.");

            RuleFor(x => x.BitisTarihi)
                .NotEmpty().WithMessage(ValidationMessageHelper.RequiredMessage("Bitiş Tarihi"))
                .GreaterThan(x => x.BaslangicTarihi).WithMessage("Bitiş tarihi, başlangıç tarihinden büyük olmalıç");
        }
    }
}
