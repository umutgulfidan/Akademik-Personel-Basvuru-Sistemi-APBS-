using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Linq;

namespace Core.Aspects.Autofac.Logger
{
    public class LoggerAspect : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // IHttpContextAccessor'ı constructor üzerinden enjekte ediyoruz
        public LoggerAspect(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        // Metodun girişinde loglama yapıyoruz
        protected override void OnBefore(IInvocation invocation)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";
            var userIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP";

            Log.Information("Entering method: {Method} with parameters: {Parameters} | User: {UserName} | IP: {UserIp}",
                invocation.Method.Name,
                string.Join(", ", invocation.Arguments.Select(arg => arg?.ToString() ?? "null")),
                userName,
                userIp);
        }

        // Hata meydana geldiğinde loglama yapıyoruz
        protected override void OnException(IInvocation invocation, Exception e)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";
            var userIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP";

            Log.Error(e, "Error occurred in method: {Method} with parameters: {Parameters} | User: {UserName} | IP: {UserIp} | Exception: {ExceptionMessage} | StackTrace: {StackTrace}",
                invocation.Method.Name,
                string.Join(", ", invocation.Arguments.Select(arg => arg?.ToString() ?? "null")),
                userName,
                userIp,
                e.Message,
                e.StackTrace);
        }

        // Başarıyla biten metotları logluyoruz
        protected override void OnSuccess(IInvocation invocation)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";
            var userIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP";

            Log.Information("Method {Method} executed successfully. Return value: {ReturnValue} | User: {UserName} | IP: {UserIp}",
                invocation.Method.Name,
                invocation.ReturnValue?.ToString() ?? "null",
                userName,
                userIp);
        }
    }
}
