
using Microsoft.EntityFrameworkCore;
using Recipe.Domain.Users;

namespace Recipe.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        // This is the property that maps your User Entity to a SQL table named "Users"
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
