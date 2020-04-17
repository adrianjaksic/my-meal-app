using Interfaces.Auth;
using Interfaces.Meals;
using Interfaces.Users;
using Microsoft.Extensions.DependencyInjection;
using Repository.Auth;
using Repository.Meals;
using Repository.Users;

namespace WebApi.StartupExtensions
{
    public static class RegisterAppServicesExtensions
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();

            services.AddScoped<IAuthService, AuthService.AuthService>();
            services.AddScoped<IMealService, MealService.MealService>();
            services.AddScoped<IUserService, UserService.UserService>();
            services.AddScoped<IUserSettingsService, UserService.UserSettingsService>();
            return services;
        }
    }
}
