using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Intuitive.Domain.Commands.AuthenticationCommands;
using MediatR;

namespace Intuitive.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommands command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommands command)
        {
            
            var response = await _mediator.Send(command).ConfigureAwait(false);

            return Ok(response);
        }
    }
}