using AuthService.Helpers;
using Enities.Auth;
using Enities.Users;
using Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUserSettingsService _userSettingsService;
        private readonly LoginModel _loggedInUser;

        public UserSettingsController(IHttpContextAccessor contextAccessor, IUserSettingsService userSettingsService)
        {
            _userSettingsService = userSettingsService;
            _loggedInUser = ClaimHelpers.GetUserFromClaims(contextAccessor.HttpContext.User.Claims);
        }

        [HttpGet("{userId}")]
        public UserSettingsModel Get(int userId)
        {
            return _userSettingsService.GetSettings(_loggedInUser, userId);
        }

        [HttpPost]
        public UserSettingsModel Post([FromBody]UserSettingsModel model)
        {
            _userSettingsService.EditSettings(_loggedInUser, model);
            return _userSettingsService.GetSettings(_loggedInUser, model.UserId);
        }
    }
}