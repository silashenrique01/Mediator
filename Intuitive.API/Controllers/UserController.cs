using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Intuitive.Domain.Interfaces;
using MediatR;
using Intuitive.Domain.Commands;

namespace Intuitive.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IIntuitiveRepository _repository;
        //private readonly ILogger<UserController> _logger;

        private readonly IMediator _mediator;

        public UserController(IMediator mediator, IIntuitiveRepository repository)
        {

            _mediator = mediator;
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new GetAllUserCommand();
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response.Content);
        }
        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get(int UserId)
        {
            var command = new GetUserByIdCommand(UserId);
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return response.Content != null ? (ActionResult)Ok(response.Content) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response.Content);
        }


        [Route("{UserId:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int UserId)
        {
            var command = new DeleteUserCommand(UserId);
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }
    }
}