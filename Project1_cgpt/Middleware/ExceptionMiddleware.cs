using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

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
            catch (DbUpdateException)
            {
                await HandleExceptionAsync(context, "Duplicate value detected.");
            }
            catch (Exception)
            {
                await HandleExceptionAsync(context, "Internal server error.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new
            {
                error = message
            });

            return context.Response.WriteAsync(result);
        }
    }
}