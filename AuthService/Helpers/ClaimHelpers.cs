using Enities.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthService.Helpers
{
    public static class ClaimHelpers
    {
        private const string LogIdClaimName = "logid";
        private const string FullNameClaimName = "fullname";

        public static Claim[] GetUserClaims(LoginModel logindData)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, logindData.UserId.ToString()),
                new Claim(LogIdClaimName, logindData.LogId.ToString()),
                new Claim(ClaimTypes.Email, logindData.Email),
                new Claim(FullNameClaimName, logindData.FullName),
                new Claim(ClaimTypes.Role, logindData.RoleName),
            };
        }

        public static LoginModel GetUserFromClaims(IEnumerable<Claim> claims)
        {
            int.TryParse(claims.GetValueFromClaims(ClaimTypes.Name), out int logId);
            int.TryParse(claims.GetValueFromClaims(LogIdClaimName), out int sid);
            return new LoginModel()
            {
                UserId = logId,
                LogId = sid,
                Email = claims.GetValueFromClaims(ClaimTypes.Email),
                FullName = claims.GetValueFromClaims(FullNameClaimName),
                RoleName = claims.GetValueFromClaims(ClaimTypes.Role),
            };
        }

        private static string GetValueFromClaims(this IEnumerable<Claim> claims, string name)
        {
            return claims.FirstOrDefault(c => c.Type == name)?.Value;
        }
    }
}
