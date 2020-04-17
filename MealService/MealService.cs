using System;
using System.Collections.Generic;
using Common.Exceptions;
using Enities.Auth;
using Enities.Meals;
using Interfaces.Meals;

namespace MealService
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public int AddMeal(LoginModel loggedInUser, MealRequest request)
        {
            ValidateObject(request, false);
            CheckPermission(loggedInUser, request.UserId, "You can't add other's meal.");

            var model = new MealModel()
            {
                Date = request.Date,
                Description = request.Description,
                Calories = request.Calories,
            };
            return _mealRepository.AddMeal(request.UserId, model);
        }

        public void EditMeal(LoginModel loggedInUser, MealRequest request)
        {
            ValidateObject(request, true);
            CheckPermission(loggedInUser, request.UserId, "You can't edit other's meal.");

            var model = new MealModel()
            {
                MealId = request.MealId,
                Date = request.Date,
                Description = request.Description,
                Calories = request.Calories,
            };
            _mealRepository.EditMeal(request.UserId, model);
        }

        public MealModel GetMeal(LoginModel loggedInUser, int userId, int mealId)
        {
            CheckPermission(loggedInUser, userId, "You can't get other's meal.");
            var meal = _mealRepository.GetMeal(userId, mealId);
            if (meal == null)
            {
                throw new NoContentException("Meal does not exists.");
            }
            return meal;
        }

        public List<MealModel> GetMeals(LoginModel loggedInUser, int userId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromTime, TimeSpan? toTime)
        {
            CheckPermission(loggedInUser, userId, "You can't get other's meals.");
            return _mealRepository.GetMeals(userId, fromDate, toDate, fromTime, toTime);
        }

        private static void ValidateObject(MealRequest request, bool existingMeal)
        {
            if (request.UserId <= 0)
            {
                throw new BadArgumentException("UserId must be provided.");
            }
            if (existingMeal && request.MealId <= 0)
            {
                throw new BadArgumentException("MealId must be provided.");
            }
            if (request.Calories < 0)
            {
                throw new BadArgumentException("Number of calories cannot be negative.");
            }
            if (request.Date == DateTime.MinValue)
            {
                throw new BadArgumentException("You must enter a valid date.");
            }
        }

        private static void CheckPermission(LoginModel loggedInUser, int mealUserId, string message)
        {
            var isAdmin = UserRoles.CheckUserRole(loggedInUser, UserRoles.Admin);
            if (!isAdmin && mealUserId != loggedInUser.UserId)
            {
                throw new UnauthorizedException(message);
            }
        }
    }
}
