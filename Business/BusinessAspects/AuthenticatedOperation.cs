using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions.Claims;
using Core.Extensions.Exceptions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Business.BusinessAspects
{
    public class AuthenticatedOperation : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            // Kullanıcı giriş yapmış mı?
            if (httpContext.User?.Identity?.IsAuthenticated != true)
            {
                throw new UnauthorizedAccessException(Messages.Unauthorized);
            }

            // Kullanıcı ID'si geçerli mi?
            var userId = httpContext.User.ClaimUserId();
            if (userId == 0)
            {
                throw new AuthorizationException("Geçersiz kullanıcı kimliği.");
            }
        }
    }
}
