using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public class Meal
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int MealId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public int Calories { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Modified { get; set; }

        public virtual User User { get; set; }
    }
}
