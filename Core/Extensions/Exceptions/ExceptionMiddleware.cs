using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Exceptions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception exception)
            {
                // Hata loglanır
                Log.Error(exception, "An unhandled exception occurred.");

                await HandleExceptionAsync(httpContext,exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext,System.Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> validationErrors;

            if(exception.GetType() == typeof(ValidationException))
            {
                message =exception.Message;
                validationErrors = ((ValidationException)exception).Errors;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    Message = message,
                    StatusCode = httpContext.Response.StatusCode,
                    ValidationErrors = validationErrors
                }.ToString());
            }
            else if (exception.GetType() == typeof(AuthorizationException))
            {
                message = exception.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                return httpContext.Response.WriteAsync(new ErrorDetails { Message = message, StatusCode = httpContext.Response.StatusCode }.ToString());
            }
            else if(exception.GetType() == typeof(Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException))
            {
                //message = exception.InnerException.Message;
                message = "Güncellemeye çalıştığınız varlık silinmiş ya da başkası tarafından erişiliyor olabilir.";
                httpContext.Response.StatusCode= (int)HttpStatusCode.BadRequest;
                return httpContext.Response.WriteAsync(new ErrorDetails { Message = message, StatusCode=httpContext.Response.StatusCode }.ToString());
            }
            else if (exception.GetType() == typeof(Microsoft.EntityFrameworkCore.DbUpdateException))
            {
                //message = exception.InnerException.Message;
                message = "Gönderilen alanlar veri tabanına eklenemiyor. Lütfen gönderilen bilgilerin doğruluğunu kontrol ediniz.";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return httpContext.Response.WriteAsync(new ErrorDetails { Message = message, StatusCode = httpContext.Response.StatusCode }.ToString());
            }
            else if (exception.GetType() == typeof(UnauthorizedAccessException))
            {
                message = exception.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return httpContext.Response.WriteAsync(new ErrorDetails { Message = message, StatusCode = httpContext.Response.StatusCode }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());

        }

    }
}
