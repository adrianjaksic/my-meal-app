using AuthService.Helpers;
using Enities.Auth;
using Enities.Meals;
using Interfaces.Meals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly LoginModel _loggedInUser;

        public MealController(IHttpContextAccessor contextAccessor, IMealService mealService)
        {
            _mealService = mealService;
            _loggedInUser = ClaimHelpers.GetUserFromClaims(contextAccessor.HttpContext.User.Claims);
        }

        [HttpPost("filter")]
        public List<MealModel> Filter([FromBody]FindMealRequest request)
        {
            return _mealService.GetMeals(_loggedInUser, request.UserId, request.FromDate, request.ToDate, request.FromTime, request.ToTime);
        }

        [HttpGet("{userId}/{mealId}")]
        public MealModel Get(int userId, int mealId)
        {
            return _mealService.GetMeal(_loggedInUser, userId, mealId);
        }

        [HttpPost]
        public MealModel Post([FromBody]MealRequest request)
        {
            if (request.MealId > 0)
            {
                _mealService.EditMeal(_loggedInUser, request);
            }
            else
            {
                request.MealId = _mealService.AddMeal(_loggedInUser, request);
            }
            return _mealService.GetMeal(_loggedInUser, request.UserId, request.MealId);
        }
    }
}