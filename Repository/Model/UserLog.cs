using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public class UserLog
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int LogId { get; set; }
        public DateTime LogInDate { get; set; }
        public DateTime? LogOutDate { get; set; }

        public virtual User User { get; set; }
    }
}
