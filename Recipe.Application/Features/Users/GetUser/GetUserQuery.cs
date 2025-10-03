using MediatR;
using Recipe.Domain.Users;


namespace Recipe.Application.Features.Users.GetUser
{
    public  record GetUserQuery(Guid UserId) : IRequest<UserProfileDto>;
}
