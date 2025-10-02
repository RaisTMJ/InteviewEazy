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

            if(request.ProfilePicture != null)
            {
                const int maxFileSize = 2 * 1024 * 1024;

                if (request.ProfilePicture.Length > maxFileSize) {
                    throw new DomainException("Image size exceeds the 2MB limit.");
                }
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };

                if (!allowedTypes.Contains(request.ProfilePicture.ContentType)) {
                    throw new DomainException("Invalid image file type,");
                }


                if (!string.IsNullOrEmpty(user.ProfilePicture))
                {
                    //await _fileStorageService.DeleteFileAsync(user.ProfilePicture);
                }

                using var stream =  request.ProfilePicture.OpenReadStream();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var originalFileName = Path.GetFileNameWithoutExtension(request.ProfilePicture.FileName);

                // Remove invalid chars
                var safeFileName = string.Concat(originalFileName.Split(Path.GetInvalidFileNameChars()));

                // Build new filename with GUID + original extension
                string newFileName = $"{Guid.NewGuid()}_{safeFileName}{Path.GetExtension(request.ProfilePicture.FileName)}";
                string newFileImageUrl = await _fileStorageService.SaveFileAsync(
                    memoryStream.ToArray(),
                    "profile-images",
                    newFileName
                    );

                user.ProfilePicture = newFileImageUrl;


            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UpdatedAt = DateTime.Now;

            await _userRepository.UpdateUserAsync(user);

            return Unit.Value;
        }
    }
}
