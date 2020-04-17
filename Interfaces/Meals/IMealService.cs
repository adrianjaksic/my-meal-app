using System;
using System.Collections.Generic;
using Enities.Auth;
using Enities.Meals;

namespace Interfaces.Meals
{
    public interface IMealService
    {
        List<MealModel> GetMeals(LoginModel loggedInUser, int userId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromTime, TimeSpan? toTime);
        MealModel GetMeal(LoginModel loggedInUser, int userId, int mealId);
        void EditMeal(LoginModel loggedInUser, MealRequest model);
        int AddMeal(LoginModel loggedInUser, MealRequest model);
    }
}
