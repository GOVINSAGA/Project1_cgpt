using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Project1_cgpt.Exceptions;
namespace Project1_cgpt.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (AuthenticationException ex)
            {
                await HandleExceptionAsync(context, ex.Message, StatusCodes.Status401Unauthorized);
            }
            catch (DbUpdateException)
            {
                await HandleExceptionAsync(context, "Duplicate value detected.", StatusCodes.Status400BadRequest);
            }

            catch (Exception)
            {
                await HandleExceptionAsync(context, "Internal server error.", StatusCodes.Status500InternalServerError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = message
            });

            return context.Response.WriteAsync(result);
        }
    }
}