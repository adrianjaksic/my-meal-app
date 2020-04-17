using System;

namespace Enities.Meals
{
    public class MealModel
    {
        public int MealId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public TimeSpan Time { get; set; }
    }
}
