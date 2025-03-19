namespace Business.Constants
{
    public class ServiceMessageHelper
    {
        // Success Messages
        public static string SuccessMessage(string action)
        {
            return $"{action} işlemi başarılı.";
        }
        public static string CustomSuccessMessage(string message)
        {
            return message;
        }
        public static string ListedMessage(string entityName)
        {
            return $"{entityName} başarıyla listelendi";
        }
        public static string CreatedMessage(string entityName)
        {
            return $"{entityName} başarıyla oluşturuldu.";
        }

        public static string UpdatedMessage(string entityName)
        {
            return $"{entityName} başarıyla güncellendi.";
        }

        public static string DeletedMessage(string entityName)
        {
            return $"{entityName} başarıyla silindi.";
        }

        // Error Messages
        public static string ErrorMessage(string action)
        {
            return $"{action} işlemi sırasında bir hata oluştu.";
        }

        public static string NotFoundMessage(string entityName)
        {
            return $"{entityName} bulunamadı.";
        }

        public static string InvalidDataMessage(string entityName)
        {
            return $"{entityName} verileri geçerli değil.";
        }

        // Conflict Messages
        public static string ConflictMessage(string entityName)
        {
            return $"{entityName} ile ilgili bir çakışma meydana geldi.";
        }

        // Custom Messages
        public static string CustomErrorMessage(string message)
        {
            return message;
        }

        public static string UnauthorizedMessage()
        {
            return "Bu işlem için yetkiniz yok.";
        }

        public static string ForbiddenMessage()
        {
            return "Bu işlem yasaklanmış.";
        }

        public static string BadRequestMessage()
        {
            return "Geçersiz istek.";
        }
    }

}
