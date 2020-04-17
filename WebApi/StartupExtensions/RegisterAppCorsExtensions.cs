using Microsoft.Extensions.DependencyInjection;

namespace WebApi.StartupExtensions
{
    public static class RegisterAppCorsExtensions
    {
        public const string AppCorsPolicyName = "AppCors";

        public static IServiceCollection RegisterAppCors(this IServiceCollection services, string clientUrl)
        {
            if (clientUrl == "*")
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(AppCorsPolicyName,
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
                });
            }
            else
            {
                var urls = clientUrl.Split(',');
                services.AddCors(options =>
                {
                    options.AddPolicy(AppCorsPolicyName,
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.WithOrigins(urls);
                    });
                });
            }
            return services;
        }
    }
}
