using Common.Exceptions;
using Enities.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace WebApi.StartupExtensions
{
    public static class RegisterExceptionHandlingExtensions
    {
        public static IApplicationBuilder RegisterExceptionHandling(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var requestUrl = $"{context.Request.Path}{context.Request.QueryString}";                    
                    string errorMessage;
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;
                        if (ex is BaseException be)
                        {
                            context.Response.StatusCode = be.StatusCode;
                        }
                        errorMessage = ex.Message;
                        logger.LogError(ex, $"RequestUrl: {requestUrl}");                        
                    }
                    else
                    {
                        errorMessage = string.Empty;
                        logger.LogError($"RequestUrl: {requestUrl}");
                    }

                    var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse()
                    {
                        Error = true,
                        RequestUrl = requestUrl,
                        ErrorMessage = errorMessage,
                    }, options));
                });
            });
            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "application/json";
                
                var requestUrl = $"{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}";
                string errorMessage;

                switch (context.HttpContext.Response.StatusCode)
                {
                    case StatusCodes.Status500InternalServerError:
                        errorMessage = "Ups. Something bad has happened on server.";
                        break;
                    case StatusCodes.Status401Unauthorized:
                        errorMessage = "Unauthorized access.";
                        break;
                    case StatusCodes.Status403Forbidden:
                        errorMessage = "You are not authorised for this operation.";
                        break;
                    case StatusCodes.Status404NotFound:
                        errorMessage = "Endpoint not found.";
                        break;
                    default:
                        errorMessage = "";
                        break;
                }

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse()
                {
                    Error = true,
                    RequestUrl = requestUrl,
                    ErrorMessage = errorMessage,
                }, options));
            });

            return app;
        }
    }
}
