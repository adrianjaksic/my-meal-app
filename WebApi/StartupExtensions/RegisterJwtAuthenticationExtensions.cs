using AuthService.Helpers;
using Interfaces.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.StartupExtensions
{
    public static class RegisterJwtAuthenticationExtensions
    {
        public static IServiceCollection RegisterJwtAuthentication(this IServiceCollection services, string secret, bool requireHttpsMetadata)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = ctx =>
                        {
                            var loginUser = ClaimHelpers.GetUserFromClaims(ctx.Principal.Claims.ToArray());
                            var authService = ctx.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                            if (!authService.CheckLogin(loginUser.UserId, loginUser.LogId))
                            {
                                ctx.Fail($"User is no longer logged in.");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = requireHttpsMetadata;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                    };
            });
            return services;
        }
    }
}
