using System.Net;
using System.Text.Json;
using Ultatel.Core.Exceptions;

namespace Ultatel.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            string result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { errors = validationException.ValidationErrors });
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = badRequestException.Message });
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { error = notFoundException.Message });
                    break;
                case NotAuthorizedException notAuthorizedException:
                    statusCode = HttpStatusCode.Forbidden;
                    result = JsonSerializer.Serialize(new { error = notAuthorizedException.Message });
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new { error = exception.Message });
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }


            return httpContext.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}