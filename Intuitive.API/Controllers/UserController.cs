using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Intuitive.Domain.Interfaces;
using MediatR;
using Intuitive.Domain.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Intuitive.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {

            _mediator = mediator;
        }

        /* As requisições são encapsuladas atráves do mediator.
        Ou seja, diferente da forma anterior não acessamos o banco de dados de forma direta
        para lermos as informações, isso torna o sistema seguro e eficaz.*/
        
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

          [HttpGet("{Name}")]
        public async Task<IActionResult> Get(string Name)
        {
            var command = new GetUserByNameCommand(Name);
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
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int UserId)
        {
            var command = new DeleteUserCommand(UserId);
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }
    }
}