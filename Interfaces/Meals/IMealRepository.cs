using Enities.Meals;
using System;
using System.Collections.Generic;

namespace Interfaces.Meals
{
    public interface IMealRepository
    {
        int AddMeal(int userId, MealModel meal);
        void EditMeal(int userId, MealModel meal);
        MealModel GetMeal(int userId, int mealId);
        List<MealModel> GetMeals(int userId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromTime, TimeSpan? toTime);
    }
}
