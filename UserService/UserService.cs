using System.Collections.Generic;
using Common.Exceptions;
using Common.Helpers;
using Enities;
using Enities.Auth;
using Enities.Users;
using Interfaces.Auth;
using Interfaces.Users;
using Microsoft.Extensions.Options;

namespace UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;

        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IAuthRepository authRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _appSettings = options.Value;
        }

        public int AddUser(LoginModel loggedInUser, UserRequest request)
        {
            ValidateObject(request, false);

            CheckPermission(loggedInUser, request.RoleName, "You can't create admin user.");

            var user = _userRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                throw new BadArgumentException("User with provided email existis.");
            }

            var model = new UserModel()
            {
                Email = request.Email,
                Password = HashPassword(request.Password),
                FullName = request.FullName,
                Active = request.Active,
                RoleName = request.RoleName,
            };
            return _userRepository.AddUser(model);
        }        

        public bool EditUser(LoginModel loggedInUser, UserRequest request)
        {
            ValidateObject(request, true);

            var user = _userRepository.GetUser(request.UserId);
            if (user == null)
            {
                throw new NoContentException("User does not exists.");
            }

            CheckPermission(loggedInUser, user.RoleName, "You can't edit admin user.");

            var model = new UserModel()
            {
                UserId = request.UserId,
                Email = user.Email,                
                FullName = request.FullName,
                Active = request.Active,
                RoleName = request.RoleName,
            };
            _userRepository.EditUser(model);
            if (request.UserId == loggedInUser.UserId && request.RoleName != user.RoleName)
            {
                _authRepository.LogoffUser(loggedInUser.UserId, loggedInUser.LogId);
                return true;
            }
            return false;
        }

        public UserModel GetUser(LoginModel loggedInUser, int userId)
        {            
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new NoContentException("User does not exists.");
            }

            CheckPermission(loggedInUser, user.RoleName, "You can't get admin user.");

            return user;
        }

        public List<UserModel> GetUsers(LoginModel loggedInUser)
        {
            var isAdmin = UserRoles.CheckUserRole(loggedInUser, UserRoles.Admin);
            return _userRepository.GetUsers(isAdmin);
        }

        private byte[] HashPassword(string password)
        {
            return SecurityHelper.HashPassword(password + _appSettings.PasswordSalt);
        }

        private static void ValidateObject(UserRequest request, bool existing)
        {
            if (!existing && string.IsNullOrEmpty(request.Email))
            {
                throw new BadArgumentException("Email must be provided.");
            }
            if (!existing && string.IsNullOrEmpty(request.Password))
            {
                throw new BadArgumentException("Password must be provided.");
            }
            if (string.IsNullOrEmpty(request.FullName))
            {
                throw new BadArgumentException("Fullname must be provided.");
            }
            if (!UserRoles.ValidateUserRole(request.RoleName))
            {
                throw new BadArgumentException("User role is not valid.");
            }
        }

        private static void CheckPermission(LoginModel loggedInUser, string roleName, string message)
        {
            var isAdmin = UserRoles.CheckUserRole(loggedInUser, UserRoles.Admin);
            if (!isAdmin && roleName == UserRoles.Admin)
            {
                throw new UnauthorizedException(message);
            }
        }
    }
}
