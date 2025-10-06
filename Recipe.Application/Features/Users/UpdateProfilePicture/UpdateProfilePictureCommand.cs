using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe.Application.Features
{
    public record UpdateProfilePictureCommand
    ( Guid UserId, IFormFile ProfilePicture) : IRequest<Unit>;
}
     
