using Recipe.Domain.Users;


namespace Recipe.Application.Interface
{
    public  interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);


    }
}
