
using MediatR;
using Microsoft.AspNetCore.Http;
namespace Recipe.Application.Features
{
    public record UpdateUserProfileCommand
    (
        Guid UserId,
        string FirstName,
        string LastName, IFormFile ProfilePicture
    ): IRequest<Unit>;
}
