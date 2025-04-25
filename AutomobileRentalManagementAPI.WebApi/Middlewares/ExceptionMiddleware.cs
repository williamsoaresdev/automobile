using System.Net.Mime;
using System.Net;
using System.Text.Json;
using AutomobileRentalManagementAPI.WebApi.Common;

using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using FluentValidation;

namespace AutomobileRentalManagementAPI.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) 
        { 
            _next = next; 
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ValidationException ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex, false);
            }
            catch(DomainException ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleCustomExceptionResponseAsync(context, ex, true);
            }
        }

        private static async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception ex, bool addStackTrace = true)
        {
            var status = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = status;


            var response = new ApiResponse() { success = false };
            if (addStackTrace)
                response = new ApiResponse() { success = false, mensagem = GetErrorString(ex) };
            else
                response = new ApiResponse() { success = false, mensagem = ex.Message };


            var json = JsonSerializer.Serialize(response, _jsonSerializerOptions);

            await context.Response.WriteAsync(json);
        }


        private static string GetErrorString(Exception e) => $"Message: {e.Message}; StackTrace: {e.StackTrace}";
    }
}
