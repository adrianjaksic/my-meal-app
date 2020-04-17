using System;
using System.Collections.Generic;
using System.Linq;
using Common.Exceptions;
using Enities.Exceptions;
using Enities.Meals;
using Interfaces.Meals;
using Microsoft.EntityFrameworkCore;
using Repository.Model;

namespace Repository.Meals
{
    public class MealRepository : BaseRepository, IMealRepository
    {
        public MealRepository(DbContextOptions<DbMealsContext> options) : base(options) { }

        public int AddMeal(int userId, MealModel meal)
        {
            using (var context = GetContext())
            {
                var dbMeal = context.Meals.SingleOrDefault(m => m.UserId == userId && m.MealId == meal.MealId);
                if (dbMeal != null)
                {
                    throw new BadArgumentException("Meal does not exists.");
                }
                var today = DateTime.Now;
                var mealId = 1;
                if (context.Meals.Any(m => m.UserId == userId))
                {
                    mealId = context.Meals.Where(m => m.UserId == userId).Max(m => m.MealId + 1);
                }
                dbMeal = new Meal()
                {
                    UserId = userId,
                    MealId = mealId,
                    Date = meal.Date.Date,
                    Time = meal.Date.TimeOfDay,
                    Description = meal.Description,
                    Calories = meal.Calories,
                    Created = today,
                    Modified = today,                    
                };
                context.Meals.Add(dbMeal);
                context.SaveChanges();
                return mealId;
            }
        }

        public void EditMeal(int userId, MealModel meal)
        {
            using (var context = GetContext())
            {
                var dbMeal = context.Meals.SingleOrDefault(m => m.UserId == userId && m.MealId == meal.MealId);
                if (dbMeal == null)
                {
                    throw new NoContentException();
                }
                var today = DateTime.Now;
                dbMeal.Date = meal.Date.Date;
                dbMeal.Time = meal.Date.TimeOfDay;
                dbMeal.Description = meal.Description;
                dbMeal.Calories = meal.Calories;
                dbMeal.Modified = today;
                context.SaveChanges();
            }
        }

        public MealModel GetMeal(int userId, int mealId)
        {
            using (var context = GetContext())
            {                
                return context.Meals.AsNoTracking().Where(m => m.UserId == userId && m.MealId == mealId).Select(m => new MealModel() 
                {
                    MealId = m.MealId,
                    Date = m.Date.Add(m.Time),
                    Time = m.Time,
                    Description = m.Description,
                    Calories = m.Calories,
                    Created = m.Created,
                    Modified = m.Modified,
                }).SingleOrDefault();
            }
        }

        public List<MealModel> GetMeals(int userId, DateTime? fromDate, DateTime? toDate, TimeSpan? fromTime, TimeSpan? toTime)
        {
            using (var context = GetContext())
            {
                var mealsQuery = context.Meals.AsNoTracking().Where(m => m.UserId == userId);

                if (fromDate != null)
                {
                    mealsQuery = mealsQuery.Where(m => m.Date >= fromDate);
                }
                if (toDate != null)
                {
                    mealsQuery = mealsQuery.Where(m => m.Date <= toDate);
                }
                if (fromTime != null)
                {
                    mealsQuery = mealsQuery.Where(m => m.Time >= fromTime);
                }
                if (toTime != null)
                {
                    mealsQuery = mealsQuery.Where(m => m.Time <= toTime);
                }

                return mealsQuery.OrderByDescending(m => m.Date).ThenByDescending(t => t.Time).Select(m => new MealModel()
                {
                    MealId = m.MealId,
                    Date = m.Date.Add(m.Time),
                    Time = m.Time,
                    Description = m.Description,
                    Calories = m.Calories,
                    Created = m.Created,
                    Modified = m.Modified,
                }).ToList();
            }
        }
    }
}
