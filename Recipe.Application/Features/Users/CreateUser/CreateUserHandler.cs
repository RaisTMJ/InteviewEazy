using MediatR;
using Recipe.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Application.Features.Users.CreateUser
{
    public  class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private IUserRepository _userRepository;
        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("User with the same email already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


            var user = new Domain.Users.User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName= request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash
            };
            await _userRepository.AddUserAsync(user);
            return user.Id;
        }
    }
}
