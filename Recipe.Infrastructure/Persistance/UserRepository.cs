using Microsoft.EntityFrameworkCore;
using Recipe.Application.Interface;
using Recipe.Domain.Users;
using Recipe.Infrastructure.Persistence;

namespace Recipe.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string identifier)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == identifier );

        }

        public async  Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user =  await _dbContext.Users.FindAsync(userId);
            return user ;
        }
    }
}
