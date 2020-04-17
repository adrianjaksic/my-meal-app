using Microsoft.EntityFrameworkCore;
using Repository.Model;

namespace Repository
{
    public class BaseRepository
    {
        protected DbContextOptions<DbMealsContext> _options;
        public BaseRepository(DbContextOptions<DbMealsContext> options)
        {
            _options = options;
        }

        protected DbMealsContext GetContext()
        {
            return new DbMealsContext(_options);
        }
    }
}
