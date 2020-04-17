using Enities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebApi.StartupExtensions;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection(nameof(AppSettings));
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddOptions()
                .AddHttpClient()
                .Configure<AppSettings>(appSettingsSection)
                .RegisterAppServices()
                .RegisterAppCors(appSettings.ClientUrl)
                .RegisterConnectionStringDatabase(appSettings.DbConnectionString)
                .RegisterJwtAuthentication(appSettings.AuthSecret, appSettings.Live)
                .AddHttpContextAccessor()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger<Startup>();
            var appSettingsSection = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            app .UseRequestResponseLogging(appSettingsSection)
                .RegisterExceptionHandling(logger)
                .MigrateDatabase()
                .UseHttpsRedirection()
                .UseRouting()
                .UseCors(RegisterAppCorsExtensions.AppCorsPolicyName)
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
