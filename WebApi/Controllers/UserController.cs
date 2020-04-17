using AuthService.Helpers;
using Enities.Auth;
using Enities.Users;
using Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Authorize(Roles = UserRoles.ManagerOrAdmin)]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private LoginModel _loggedInUser;

        public UserController(IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _userService = userService;
            _loggedInUser = ClaimHelpers.GetUserFromClaims(contextAccessor.HttpContext.User.Claims);
        }

        [HttpGet]
        public List<UserModel> GetAll()
        {            
            return _userService.GetUsers(_loggedInUser);
        }

        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            return _userService.GetUser(_loggedInUser, id);
        }

        [HttpPost]
        public UserModel Post([FromBody]UserRequest request)
        {
            var changedRole = false;
            if (request.UserId > 0)
            {
                changedRole =_userService.EditUser(_loggedInUser, request);
            }
            else
            {
                request.UserId = _userService.AddUser(_loggedInUser, request);
            }            
            var user = _userService.GetUser(_loggedInUser, request.UserId);
            user.ChangedRole = changedRole;
            return user;
        }
    }
}