using AuthService;
using Common.Exceptions;
using Common.Helpers;
using Enities;
using Enities.Auth;
using Interfaces.Auth;
using Interfaces.Users;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class AuthServiceTest
    {
        private string _userEmail = "test@email.com";
        private string _userPassword = "test123";
        private string _userFullName = "Test User";
        private string _userRoleName = UserRoles.User;
        private int _loggedUserId = 21;

        private IAuthService _authService;

        public void SetupLoginTests()
        {
            var someOptions = Options.Create(new AppSettings());
            someOptions.Value.AuthSecret = "ABCDEFGHIJKLMNOP";
            someOptions.Value.AuthTokenExpiresInHours = 24;
            someOptions.Value.PasswordSalt = "PASSWORDSALT";

            var mockAuthRepository = new Mock<IAuthRepository>();
            var hashedPassword = SecurityHelper.HashPassword(_userPassword + someOptions.Value.PasswordSalt);
            mockAuthRepository.Setup(p => p.LoginUser(_userEmail, hashedPassword, It.IsAny<bool>())).Returns(new LoginModel() { UserId = _loggedUserId, Email = _userEmail, FullName = _userFullName, RoleName = _userRoleName, LogId = 1 });
            var mockUserRepository = new Mock<IUserRepository>();

            _authService = new AuthService.AuthService(mockAuthRepository.Object, mockUserRepository.Object, someOptions);
        }

        [TestMethod]
        public void SuccessLoginMetodTest()
        {
            SetupLoginTests();
            var login = _authService.LoginUser(_userEmail, _userPassword);
            Assert.IsNotNull(login.Token);
            Assert.IsTrue(login.UserId == _loggedUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public void FailLoginMetodTest()
        {
            SetupLoginTests();
            var login = _authService.LoginUser(_userEmail, _userPassword + "1");
        }
    }
}
