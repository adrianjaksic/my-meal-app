using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(20)]
        public byte[] Password { get; set; }
        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }
        public bool Active { get; set; }

        public virtual UserMealSettings UserMealSettings { get; set; }
        public virtual IEnumerable<UserLog> UserLogs { get; set; }
        public virtual IEnumerable<Meal> Meals { get; set; }
    }
}