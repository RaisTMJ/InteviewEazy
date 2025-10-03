using MediatR;
using Recipe.Application.Interface;
using Recipe.Domain.Exceptions;
using Recipe.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Application.Features.Users.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserProfileDto>
    {

        private readonly IUserRepository _userRepository;
        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        async Task<UserProfileDto> IRequestHandler<GetUserQuery, UserProfileDto>.Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            if (user == null)
            {
                throw new DomainException("User not found");
            }
            return new UserProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ProfileImageUrl = user.ProfilePicture
            };
        }
    }
}
