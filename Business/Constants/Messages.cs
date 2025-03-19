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
        // ALAN
        public static string AlanAdded = ServiceMessageHelper.CreatedMessage("Alan");
        public static string AlanDeleted = ServiceMessageHelper.DeletedMessage("Alan");
        public static string AlanListed = ServiceMessageHelper.ListedMessage("Alanlar");
        public static string AlanUpdated = ServiceMessageHelper.UpdatedMessage("Alan");
        public static string AlanNotFound = ServiceMessageHelper.NotFoundMessage("Alan");
        #endregion
        #region Bolum
        // BOLUM
        public static string BolumAdded = ServiceMessageHelper.CreatedMessage("Bölüm");
        public static string BolumDeleted = ServiceMessageHelper.DeletedMessage("Bölüm");
        public static string BolumListed = ServiceMessageHelper.ListedMessage("Bölümler");
        public static string BolumUpdated = ServiceMessageHelper.UpdatedMessage("Bölüm");
        public static string BolumNotFound = ServiceMessageHelper.NotFoundMessage("Bölüm");
        #endregion
        #region Pozisyon
        // POZISYOM
        public static string PozisyonAdded = ServiceMessageHelper.CreatedMessage("Pozisyon");
        public static string PozisyonDeleted = ServiceMessageHelper.DeletedMessage("Pozisyon");
        public static string PozisyonListed = ServiceMessageHelper.ListedMessage("Pozisyonlar");
        public static string PozisyonUpdated = ServiceMessageHelper.UpdatedMessage("Pozisyon");
        public static string PozisyonNotFound = ServiceMessageHelper.NotFoundMessage("Pozisyon");
        #endregion
        #region Ilan
        // ILAN
        public static string IlanAdded = ServiceMessageHelper.CreatedMessage("İlan");
        public static string IlanDeleted = ServiceMessageHelper.DeletedMessage("İlan");
        public static string IlanListed = ServiceMessageHelper.ListedMessage("İlanlar");
        public static string IlanUpdated = ServiceMessageHelper.UpdatedMessage("İlan");
        public static string IlanNotFound = ServiceMessageHelper.NotFoundMessage("İlan");
        public static string IlanActivate = "İlan başarıyla aktif edildi.";
        public static string IlanDeactivate = "İlan başarıyla deaktif edildi.";
        #endregion
        #region Auth
        // AUTH
        public static string UserPassiveAccount = "Bu hesaba erişilemiyor. Kullanıcı hesabı yasaklanmış ya da silinmiş olabilir.";
        public static string UserPasswordError = "Tc No ya da şifre hatalı.";
        public static string SuccessfulLogin = "Başarıyla giriş yapıldı.";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public static string InvalidUser = "Gerçek bir kullanıcı değil.";
        public static string TokenCreated = "Token başarıyla oluşturuldu.";
        #endregion
        #region Operation Claim
        // OPERATION CLAIM
        public static string OperationClaimAdded = ServiceMessageHelper.CreatedMessage("Rol");
        public static string OperationClaimDeleted = ServiceMessageHelper.DeletedMessage("Rol");
        public static string OperationClaimListed = ServiceMessageHelper.ListedMessage("Roller");
        public static string OperationClaimUpdated = ServiceMessageHelper.UpdatedMessage("Rol");
        public static string OperationClaimNotFound = ServiceMessageHelper.NotFoundMessage("Rol");
        #endregion
        #region User
        // USER
        public static string UserAdded = ServiceMessageHelper.CreatedMessage("Kullanıcı");
        public static string UserDeleted = ServiceMessageHelper.DeletedMessage("Kullanıcı");
        public static string UserListed = ServiceMessageHelper.ListedMessage("Kullanıcı");
        public static string UserUpdated = ServiceMessageHelper.UpdatedMessage("Kullanıcı");
        public static string UserNotFound = ServiceMessageHelper.NotFoundMessage("Kullanıcı");
        public static string UserActivate = "Kullanıcı hesabı başarıyla aktif edildi.";
        public static string UserDeactivate = "Kullanıcı hesabı başarıyla deaktif edildi.";
        #endregion
        #region User Operation Claim
        // USER OPERATION CLAIM
        public static string UserOperationClaimAdded = ServiceMessageHelper.CreatedMessage("Rol");
        public static string UserOperationClaimDeleted = ServiceMessageHelper.DeletedMessage("Rol");
        public static string UserOperationClaimListed = ServiceMessageHelper.ListedMessage("Roller");
        public static string UserOperationClaimUpdated = ServiceMessageHelper.UpdatedMessage("Rol");
        public static string UserOperationClaimNotFound = ServiceMessageHelper.NotFoundMessage("Rol");
        #endregion
    }

}
