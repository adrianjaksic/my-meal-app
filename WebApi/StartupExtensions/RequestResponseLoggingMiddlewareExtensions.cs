using Enities;
using Microsoft.AspNetCore.Builder;
using WebApi.Middlewears;

namespace WebApi.StartupExtensions
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder, AppSettings appSettings)
        {
            if (appSettings.UseRequestLogging)
            {
                builder.UseMiddleware<RequestResponseLoggingMiddleware>();
            }
            return builder;
        }
    }
}
