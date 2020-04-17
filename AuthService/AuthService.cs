using AuthService.Helpers;
using Common.Exceptions;
using Common.Helpers;
using Enities;
using Enities.Auth;
using Enities.Users;
using Interfaces.Auth;
using Interfaces.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        public AuthService(IAuthRepository authRepository, IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _appSettings = options.Value;
        }

        public LoginModel LoginUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new BadArgumentException("Email must be provided.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new BadArgumentException("Password must be provided.");
            }
            byte[] hashedPassword = HashPassword(password);
            var logindData = _authRepository.LoginUser(email, hashedPassword, _appSettings.LogOutOldLogs);
            if (logindData == null)
            {
                throw new UnauthorizedException("Email or password is not valid.");
            }

            logindData.Token = GetJwtToken(logindData);

            return logindData;
        }

        public void LogoffUser(int userId, int logId)
        {
            if (!_authRepository.LogoffUser(userId, logId))
            {
                throw new NoContentException("User is already logged off.");
            }
        }

        public LoginModel RegisterUser(RegisterRequest model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new BadArgumentException("Email must be provided.");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new BadArgumentException("Password must be provided.");
            }
            if (string.IsNullOrEmpty(model.FullName))
            {
                throw new BadArgumentException("Fullname must be provided.");
            }
            var userModel = new UserModel()
            {
                Email = model.Email,
                FullName = model.FullName,
                Password = HashPassword(model.Password),
                RoleName = UserRoles.User,
                Active = true,
            };
            _userRepository.AddUser(userModel);
            return LoginUser(model.Email, model.Password);
        }

        public bool CheckLogin(int userId, int logId)
        {
            return _authRepository.CheckLogin(userId, logId);
        }

        private byte[] HashPassword(string password)
        {
            return SecurityHelper.HashPassword(password + _appSettings.PasswordSalt);
        }

        private string GetJwtToken(LoginModel logindData)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.AuthSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(ClaimHelpers.GetUserClaims(logindData)),
                Expires = DateTime.UtcNow.AddHours(_appSettings.AuthTokenExpiresInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
