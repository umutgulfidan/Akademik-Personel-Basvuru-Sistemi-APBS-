using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var stopwatch = Stopwatch.StartNew();

            Log.Information("Request started: {Method} {Url} from {IP}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.Connection.RemoteIpAddress);

            await _next(httpContext);

            stopwatch.Stop();
            Log.Information("Request ended: {Method} {Url} with status code {StatusCode} in {ElapsedTime} ms",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
