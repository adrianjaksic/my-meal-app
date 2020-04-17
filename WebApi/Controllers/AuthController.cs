using AuthService.Helpers;
using Enities;
using Enities.Auth;
using Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [Authorize()]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
       
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public LoginModel Register([FromBody]RegisterRequest model)
        {
            return _authService.RegisterUser(model);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public LoginModel Login([FromBody]LoginRequest model)
        {
            return _authService.LoginUser(model.Email, model.Password);
        }

        [HttpPost("logout")] 
        public BaseModel Logout()
        {
            var user = ClaimHelpers.GetUserFromClaims(User.Claims.ToArray());
            _authService.LogoffUser(user.UserId, user.LogId);
            return new BaseModel() { Error = false };
        }
    }
}