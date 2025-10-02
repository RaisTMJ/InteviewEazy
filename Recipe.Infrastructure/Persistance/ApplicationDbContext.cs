
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            string adminPasswordHash = "$2a$11$DTjutr9lesxUJqX.05P1we7.9eIZ5aXALiDMaJj8bdLi87NgD0PV2";
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("C9120612-921C-4B7C-A5A9-C59714E8064F"),
                //Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Email = "admin@example.com",
                PasswordHash = adminPasswordHash,
                FirstName = "Admin",
                LastName = "User",
                ProfilePicture = null,
                UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)

            });

        }
    }
}
