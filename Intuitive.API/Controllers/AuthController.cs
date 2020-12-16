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



         ///<summary>
        /// Registra Usuário com token de autenticação
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Todo
        ///     {
        ///        "fullname": "Usuário",
        ///        "email": "exemplo@exemplo.com",
        ///        "username": "teste",
        ///        "password": "123456"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo usuário criado ou acesso concedido</returns>
        /// <response code="201">Retorna o novo usuário criado</response>
        /// <response code="400">Se o usuário não for criado</response>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommands command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }


        
        ///<summary>
        /// Realiza o login
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Todo
        ///     {
        ///        "username": "teste",
        ///        "password": "123456"
        ///     }
        ///
        /// </remarks>
        /// <returns>acesso concedido ou acesso negado</returns>
        /// <response code="200">Retorna se o acesso for concedido</response>
        /// <response code="400">Retorna se o acesso não for concedido</response> 
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommands command)
        {
            
            var response = await _mediator.Send(command).ConfigureAwait(false);

            return Ok(response);
        }
    }
}