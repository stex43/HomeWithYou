using System;
using System.Text.Json;
using System.Threading.Tasks;
using HomeWithYou.Client.Models.Errors;
using Microsoft.AspNetCore.Http;

namespace HomeWithYou.API.Infrastructure
{
    public sealed class ExceptionHandlingMiddleware
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true
        };
        
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new Error
            {
                StatusCode = 500, 
                Message = exception.Message
            };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    error.StatusCode = StatusCodes.Status404NotFound;
                    error.Target = notFoundException.Target;
                    break;
                
                case ConflictException:
                    error.StatusCode = StatusCodes.Status409Conflict;
                    break;
            }

            context.Response.StatusCode = error.StatusCode;
            var serializedError = JsonSerializer.Serialize(error, JsonSerializerOptions);
            return context.Response.WriteAsync(serializedError);
        }
    }
}