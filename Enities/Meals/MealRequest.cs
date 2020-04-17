using System;

namespace Enities.Meals
{
    public class MealRequest
    {
        public int UserId { get; set; }
        public int MealId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
    }
}
