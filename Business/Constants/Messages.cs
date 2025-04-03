using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        #region Alan
        public static string AlanAdded = ServiceMessageHelper.CreatedMessage("Alan");
        public static string AlanDeleted = ServiceMessageHelper.DeletedMessage("Alan");
        public static string AlanListed = ServiceMessageHelper.ListedMessage("Alanlar");
        public static string AlanUpdated = ServiceMessageHelper.UpdatedMessage("Alan");
        public static string AlanNotFound = ServiceMessageHelper.NotFoundMessage("Alan");
        #endregion
        #region Bolum
        public static string BolumAdded = ServiceMessageHelper.CreatedMessage("Bölüm");
        public static string BolumDeleted = ServiceMessageHelper.DeletedMessage("Bölüm");
        public static string BolumListed = ServiceMessageHelper.ListedMessage("Bölümler");
        public static string BolumUpdated = ServiceMessageHelper.UpdatedMessage("Bölüm");
        public static string BolumNotFound = ServiceMessageHelper.NotFoundMessage("Bölüm");
        #endregion
        #region Pozisyon
        public static string PozisyonAdded = ServiceMessageHelper.CreatedMessage("Pozisyon");
        public static string PozisyonDeleted = ServiceMessageHelper.DeletedMessage("Pozisyon");
        public static string PozisyonListed = ServiceMessageHelper.ListedMessage("Pozisyonlar");
        public static string PozisyonUpdated = ServiceMessageHelper.UpdatedMessage("Pozisyon");
        public static string PozisyonNotFound = ServiceMessageHelper.NotFoundMessage("Pozisyon");
        #endregion
        #region Ilan
        public static string IlanAdded = ServiceMessageHelper.CreatedMessage("İlan");
        public static string IlanDeleted = ServiceMessageHelper.DeletedMessage("İlan");
        public static string IlanListed = ServiceMessageHelper.ListedMessage("İlanlar");
        public static string IlanUpdated = ServiceMessageHelper.UpdatedMessage("İlan");
        public static string IlanNotFound = ServiceMessageHelper.NotFoundMessage("İlan");
        public static string IlanActivate = "İlan başarıyla aktif edildi.";
        public static string IlanDeactivate = "İlan başarıyla deaktif edildi.";
        #endregion
        #region Bildirimler
        public static string BildirimAdded = ServiceMessageHelper.CreatedMessage("Bildirim");
        public static string BildirimDeleted = ServiceMessageHelper.DeletedMessage("Bildirim");
        public static string BildirimListed = ServiceMessageHelper.ListedMessage("Bildirimler");
        public static string BildirimUpdated = ServiceMessageHelper.UpdatedMessage("Bildirim");
        public static string BildirimNotFound = ServiceMessageHelper.NotFoundMessage("Bildirim");
        #endregion
        #region Auth
        public static string UserPassiveAccount = "Bu hesaba erişilemiyor. Kullanıcı hesabı yasaklanmış ya da silinmiş olabilir.";
        public static string UserPasswordError = "Hesabın size ait olduğunu doğrulayamadık. Lütfen tekrar deneyin.";
        public static string SuccessfulLogin = "Başarıyla giriş yapıldı.";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public static string InvalidUser = "Gerçek bir kullanıcı değil.";
        public static string TokenCreated = "Token başarıyla oluşturuldu.";
        public static string Unauthorized = "Bu işleve giriş yapılmadan erişilemez.";
        public static string EmailAlreadyExists = "Bu email adresi zaten kullanımda";
        #endregion
        #region Operation Claim
        public static string OperationClaimAdded = ServiceMessageHelper.CreatedMessage("Rol");
        public static string OperationClaimDeleted = ServiceMessageHelper.DeletedMessage("Rol");
        public static string OperationClaimListed = ServiceMessageHelper.ListedMessage("Roller");
        public static string OperationClaimUpdated = ServiceMessageHelper.UpdatedMessage("Rol");
        public static string OperationClaimNotFound = ServiceMessageHelper.NotFoundMessage("Rol");
        #endregion
        #region User
        public static string UserAdded = ServiceMessageHelper.CreatedMessage("Kullanıcı");
        public static string UserDeleted = ServiceMessageHelper.DeletedMessage("Kullanıcı");
        public static string UserListed = ServiceMessageHelper.ListedMessage("Kullanıcı");
        public static string UserUpdated = ServiceMessageHelper.UpdatedMessage("Kullanıcı");
        public static string UserNotFound = ServiceMessageHelper.NotFoundMessage("Kullanıcı");
        public static string UserActivate = "Kullanıcı hesabı başarıyla aktif edildi.";
        public static string UserDeactivate = "Kullanıcı hesabı başarıyla deaktif edildi.";
        #endregion
        #region User Operation Claim
        public static string UserOperationClaimAdded = ServiceMessageHelper.CreatedMessage("Rol");
        public static string UserOperationClaimDeleted = ServiceMessageHelper.DeletedMessage("Rol");
        public static string UserOperationClaimListed = ServiceMessageHelper.ListedMessage("Roller");
        public static string UserOperationClaimUpdated = ServiceMessageHelper.UpdatedMessage("Rol");
        public static string UserOperationClaimNotFound = ServiceMessageHelper.NotFoundMessage("Rol");
        #endregion
        #region Kriter
        public static string KriterAdded = ServiceMessageHelper.CreatedMessage("Kriter");
        public static string KriterDeleted = ServiceMessageHelper.DeletedMessage("Kriter");
        public static string KriterListed = ServiceMessageHelper.ListedMessage("Kriterler");
        public static string KriterUpdated = ServiceMessageHelper.UpdatedMessage("Kriter");
        public static string KriterNotFound = ServiceMessageHelper.NotFoundMessage("Kriter");
        #endregion

        #region Alan Kriteri
        public static string AlanKriteriAdded = ServiceMessageHelper.CreatedMessage("Alan Kriteri");
        public static string AlanKriteriDeleted = ServiceMessageHelper.DeletedMessage("Alan Kriteri");
        public static string AlanKriteriListed = ServiceMessageHelper.ListedMessage("Alan Kriterleri");
        public static string AlanKriteriUpdated = ServiceMessageHelper.UpdatedMessage("Alan Kriteri");
        public static string AlanKriteriNotFound = ServiceMessageHelper.NotFoundMessage("Alan Kriteri");
        #endregion

        #region Puan Kriteri
        public static string PuanKriteriAdded = ServiceMessageHelper.CreatedMessage("Puan Kriteri");
        public static string PuanKriteriDeleted = ServiceMessageHelper.DeletedMessage("Puan Kriteri");
        public static string PuanKriteriListed = ServiceMessageHelper.ListedMessage("Puan Kriterleri");
        public static string PuanKriteriUpdated = ServiceMessageHelper.UpdatedMessage("Puan Kriteri");
        public static string PuanKriteriNotFound = ServiceMessageHelper.NotFoundMessage("Puan Kriteri");
        #endregion
    }

}
