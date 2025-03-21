using Castle.DynamicProxy;
using Core.Extensions.Claims;
using Core.Extensions.Exceptions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Business.BusinessAspects
{
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly string _userIdPropertyName;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles, string userIdPropertyName = null)
        {
            _roles = roles.Split(',');
            _userIdPropertyName = userIdPropertyName;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.User?.Identity?.IsAuthenticated != true)
            {
                throw new UnauthorizedAccessException("Bu işleve giriş yapmadan erişilemez.");
            }

            var roleClaims = httpContext.User.ClaimRoles();
            var userId = httpContext.User.ClaimUserId(); // Kullanıcının kendi ID’sini çekiyoruz


            // 1️⃣ **Yetkili rollerden birine sahipse işlemi yap**
            if (_roles.Any(role => roleClaims.Contains(role)))
            {
                return;
            }

            // Eğer belirtilmişse, kullanıcının ID’si metodun parametrelerinden biriyle eşleşiyor mu?**
            if (!string.IsNullOrEmpty(_userIdPropertyName))
            {
                var argument = invocation.Arguments.FirstOrDefault(arg =>
                    arg != null &&
                    arg.GetType().GetProperty(_userIdPropertyName) != null);

                if (argument != null)
                {
                    var propertyValue = argument.GetType().GetProperty(_userIdPropertyName).GetValue(argument)?.ToString();
                    if (Convert.ToInt32(propertyValue) == userId)
                    {
                        return; // Kullanıcı kendi verisini güncelliyorsa izin ver
                    }
                }
            }


            // ❌ Yetkisiz erişim
            throw new AuthorizationException("Yetkisiz Erişim!");
        }
    }
}
