using MediatR;
using Recipe.Application.Interface;
using Recipe.Domain.Exceptions;
using BCrypt.Net;
using Recipe.Application.Interfaces;

namespace Recipe.Application.Features
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string> {

        private readonly IUserRepository _userRepository;

        private readonly ITokenService _tokenService;
    public LoginQueryHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
            _tokenService = tokenService;
    }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user is null)
            {
                throw new DomainException("Invalid Credential");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new DomainException("Invalid Credentials");
            }
            string token = _tokenService.GenerateToken(user);
            return token;
        }
    }
}
