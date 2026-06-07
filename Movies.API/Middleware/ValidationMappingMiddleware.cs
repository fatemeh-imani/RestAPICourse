using FluentValidation;
using Movies.Applications.Validator;
using System.Net;

namespace Movies.API.Middleware
{
    public sealed class ValidationMappingMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidationMappingMiddleware(RequestDelegate next)
       => _next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                // اگر Response شروع شده باشد دیگر نمی‌شود بدنه/کد را تغییر داد
                if (context.Response.HasStarted)
                    throw;

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                    .ToList();

                var response = new ValidationErrorResponse(errors);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
