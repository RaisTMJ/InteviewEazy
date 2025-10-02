using Recipe.Domain.Users;


namespace Recipe.Application.Interface
{
    public  interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> GetUserById(Guid userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);


    }
}
