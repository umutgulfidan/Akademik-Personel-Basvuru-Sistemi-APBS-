using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class CustomValidatorRules
    {
        private static readonly string allowedSpecialCharacters = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";

        // Tc Kimik No 0 ile başlayamaz
        public static bool StartsWithNonZero(string input)
        {
            return !input.StartsWith("0");
        }

        // Sayı İçeriyor mu?
        public static bool ContainsDigit(string input)
        {
            return input.Any(char.IsDigit);
        }

        // Küçük harf içeriyor mu ?
        public static bool ContainsLowerCase(string input)
        {
            return input.Any(char.IsLower);
        }
        public static bool ContainsUpperCase(string input)
        {
            return input.Any(char.IsUpper);
        }

        // Belirtilen özel karakterlerden birini içeriyor mu?
        public static bool ContainsAllowedSpecialCharacter(string input)
        {
            return input.Any(ch => allowedSpecialCharacters.Contains(ch));
        }

    }


}
