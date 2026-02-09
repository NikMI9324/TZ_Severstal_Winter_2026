using Microsoft.AspNetCore.Mvc;
using Npgsql.Replication.PgOutput.Messages;
using Severstal.Domain.Exeptions;
using System.Reflection;
using System.Text.Json;

namespace Severstal.Api.MIddleware
{
    public class GlobalExpceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExpceptionMiddleware(RequestDelegate next) => _next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ProblemDetails details;
            switch(ex)
            {
                case RollNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    details = new ProblemDetails
                    {
                        Status = 404,
                        Detail = ex.Message,
                        Title = "Не найдено"
                    };
                    break;
                case RollAlreadyRemovedException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    details = new ProblemDetails
                    {
                        Status = 409,
                        Detail = ex.Message,
                        Title = "Конфликт"
                    };
                    break;

                case InvalidDataRollException:
                case InvalidRangeValuesException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    details = new ProblemDetails
                    {
                        Status = 400,
                        Title = "Неудачный запрос",
                        Detail = ex.Message
                    };
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    details = new ProblemDetails
                    {
                        Status = 500,
                        Detail = "Произошла непредвиденная ошибка на сервере",
                        Title = "Ошибка сервера"
                    };
                    break;
            }

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(details, options));
        }
    }
}
