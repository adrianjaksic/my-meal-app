using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApi.StartupExtensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection RegisterConnectionStringDatabase(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddDbContext<DbMealsContext>(options => options.UseSqlServer(dbConnectionString));
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DbMealsContext>();
                context.Database.Migrate();
            }
            return builder;
        }
    }
}
