using Recipe.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Recipe.API.Middleware
{
    public class ExceptionMiddleware
    {
       private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try {
                await _next(httpContext);
            } catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            } 
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode =  (int) HttpStatusCode.InternalServerError;


            if(exception is DomainException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            var result =  JsonSerializer.Serialize( new { 
                statusCode = context.Response.StatusCode,
                message = exception?.Message 
            });

            return context.Response.WriteAsync(result);
        }
    }
}
