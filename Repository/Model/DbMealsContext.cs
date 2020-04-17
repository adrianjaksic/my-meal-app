using Enities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Repository.Model
{
    public partial class DbMealsContext : DbContext
    {
        public DbMealsContext(DbContextOptions<DbMealsContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserMealSettings> UserMealSettings { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(b => b.Email)
                .IsUnique();

            modelBuilder.Entity<Meal>()
                .HasKey(c => new { c.UserId, c.MealId });

            modelBuilder.Entity<Meal>()
                .HasIndex(b => new { b.UserId, b.Date });

            modelBuilder.Entity<UserLog>()
                .HasKey(c => new { c.UserId, c.LogId });
        }
    }
}
