using MediatR;
using Recipe.Application.Interface;
using Recipe.Application.Interfaces;
using Recipe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Application.Features
{
    public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, Unit>
    {
        private IUserRepository _userRepository;
        private IFileStorageService _fileStorageService;
        public UpdateUserProfileHandler(IUserRepository userRepository, IFileStorageService fileStorageService)
        {
            _userRepository = userRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Unit> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user  = await _userRepository.GetUserById(request.UserId);

            if (user == null)
            {
                throw new DomainException("User not found");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UpdatedAt = DateTime.Now;

            await _userRepository.UpdateUserAsync(user);

            return Unit.Value;
        }
    }
}
