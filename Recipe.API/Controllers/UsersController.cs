using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipe.Application.Features;
using Recipe.Application.Features.Users.GetUser;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
          }


        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserProfile()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
            {
                return Unauthorized();
            }
            Guid userId = Guid.Parse(userIdClaim.Value);

                var query = new GetUserQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileCommand command)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim is null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Use the authenticated user's ID
            var updateCommand = command with { UserId = Guid.Parse(userIdClaim.Value) };
            
            await _mediator.Send(updateCommand);
            return Ok(new { message = "Profile updated successfully" });
        }


        [HttpPost("profile-picture")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile file)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
            {
                return Unauthorized();
            }
            Guid userId = Guid.Parse(userIdClaim.Value);
            if (userIdClaim is null)
            {
                return Unauthorized();
            }

            var result = new UpdateProfilePictureCommand(userId, file);

            await _mediator.Send(result);
            return Ok();
        }


    }
}
