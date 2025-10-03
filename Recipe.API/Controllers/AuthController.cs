using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipe.Application.Features;

namespace Recipe.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            string token = await _mediator.Send(query);
            return Ok(token);
        }
    }
}
