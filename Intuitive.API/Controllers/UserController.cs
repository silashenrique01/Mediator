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
        

        /// <summary>
        /// Lista usuários
        /// </summary>
        /// <returns>Todos os usuários cadastrados no sistema</returns>
        /// <response code="200">Retorna todos os usuários</response>
        /// <response code="400">Não Retorna nenhum usuários</response> 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new GetAllUserCommand();
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response.Content);
        }


        /// <summary>
        /// Lista usuário por ID
        /// </summary>
        /// <params name="UserId"></params>
        /// <returns>Retorna usuário de acordo com o ID informado</returns>
        /// <response code="200">se encontrar o ID informado</response>
        /// <response code="400">se não encontrar o ID informado</response> 
        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get(int UserId)
        {
            var command = new GetUserByIdCommand(UserId);
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return response.Content != null ? (ActionResult)Ok(response.Content) : NotFound();
        }


        /// <summary>
        /// Lista usuário por Nome
        /// </summary>
        /// <params name="name"></params>
        /// <returns>Retorna usuário de acordo com o Nome informado</returns>
        /// <response code="200">se encontrar o Nome informado</response>
        /// <response code="400">se não encontrar o Nome informado</response> 
          [HttpGet("{Name}")]
        public async Task<IActionResult> Get(string Name)
        {
            var command = new GetUserByNameCommand(Name);
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return response.Content != null ? (ActionResult)Ok(response.Content) : NotFound();
        }


        
        ///<summary>
        /// Registra Usuário no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /user
        ///     {
        ///        "fullname": "Usuário",
        ///        "DtNasc": "02/11/1999",
        ///        "email": "exemplo@exemplo.com",
        ///        "username": "teste",
        ///        "password": "123456"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo usuário criado</returns>
        /// <response code="201">Retorna o novo usuário criado</response>
        /// <response code="400">Se o usuário não for criado</response> 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }


        ///<summary>
        /// Edita Usuário no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /user
        ///     {
        ///        "fullname": "Usuário",
        ///        "DtNasc": "02/11/1999",
        ///        "email": "exemplo@exemplo.com",
        ///        "username": "teste",
        ///        "password": "123456"
        ///     }
        ///
        /// </remarks>
        /// <params name="UserId"></params>
        /// <returns>Retorna o usuário Editado</returns>
        /// <response code="201">Se o usuário for Editado</response>
        /// <response code="400">Se o usuário não for Editado</response> 
        [HttpPut("{UserId}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response.Content);
        }

        

        /// <summary>
        /// Deleta usuário atráves do ID
        /// </summary>
        /// <params name="UserId"></params>
        /// <returns>Deleta usuário de acordo com o ID informado</returns>
        /// <response code="200">se encontrar o ID informado</response>
        /// <response code="400">se não encontrar o ID informado</response> 
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