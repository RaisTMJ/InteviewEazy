using MediatR;
namespace Recipe.Application.Features;
public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<Guid>;
