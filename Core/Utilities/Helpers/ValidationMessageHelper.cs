using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class ValidationMessageHelper
    {
        public static string RequiredMessage(string propertyName)
        {
            return $"{propertyName} alanı boş geçilemez.";
        }

        public static string MinLengthMessage(string propertyName, int minLength)
        {
            return $"{propertyName} alanı en az {minLength} karakter olmalı.";
        }
        public static string MaxLengthMessage(string propertyName, int minLength)
        {
            return $"{propertyName} alanı en fazla {minLength} karakter olmalı.";
        }
        public static string ExactLengthMessage(string propertyName, int length)
        {
            return $"{propertyName} alanı tam olarak {length} karakter olmalıdır.";
        }
        public static string RangeMessage(string propertyName, int min, int max)
        {
            return $"{propertyName} alanı {min} ile {max} arasında olmalı.";
        }

        public static string GreaterThanMessage(string propertyName, int value)
        {
            return $"{propertyName} alanı {value}'den büyük olmalı.";
        }
        public static string LessThanMessage(string propertyName, int value)
        {
            return $"{propertyName} alanı {value}'den küçük olmalı.";
        }
        public static string EmailMessage(string propertyName)
        {
            return $"{propertyName} alanı geçerli bir e-posta adresi olmalı.";
        }
        public static string InvalidFormatMessage(string propertyName)
        {
            return $"{propertyName} alanı geçerli formatta değil.";
        }
    }
}
