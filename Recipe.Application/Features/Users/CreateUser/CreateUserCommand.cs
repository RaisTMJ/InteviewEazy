using MediatR;
namespace Recipe.Application.Features.Users.CreateUser;
public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<Guid>;
