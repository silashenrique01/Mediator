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

        public UserController(IMediator mediator,  IIntuitiveRepository repository)
        {
           
            _mediator = mediator;
            _repository = repository;
        }


         [HttpGet]
        public async Task<IActionResult> Get()
        {
            
        }
        /*
                  [HttpGet("{UserId}")]
                  public async Task<IActionResult> Get(int UserId)
                  {
                      try
                      {
                          var user = await _repository.GetUsersAsyncById(UserId);
                          var results = _mapper.Map<UserDto>(user);

                          return Ok(results);
                      }
                      catch (System.Exception)
                      {
                          return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
                      }
                  }

                  [HttpGet("getByName/{name}")]
                  public async Task<IActionResult> Get(string name)
                  {
                      try
                      {
                          var users = await _repository.GetAllUsersAsyncByName(name);
                          var results = _mapper.Map<IEnumerable<UserDto>>(users);

                          return Ok(results);
                      }
                      catch (System.Exception)
                      {
                          return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
                      }
                  }
           */
        [HttpPost]
       public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }

        /* 
                [HttpPut("{UserId}")]
                public async Task<IActionResult> Put(int UserId, UserDto model)
                {
                    try
                    {
                        var user = await _repository.GetUsersAsyncById(UserId);
                        if (user == null) return NotFound();

                        user.Password = _repository.GerarHashMd5(user.Password);

                        _mapper.Map(model, user);


                        _repository.Update(user);


                        if (await _repository.SaveChancesAsync())
                        {
                            return Created($"/user/{model.UserId}", _mapper.Map<UserDto>(user));
                        }

                    }
                    catch (System.Exception)
                    {
                        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
                    }

                    return BadRequest();
                }

                [HttpDelete("{Name}")]
                public async Task<IActionResult> Delete(string Name)
                {
                    try
                    {

                        var user = await _repository.GetAllUsersAsyncByName(Name);

                        if (user == null) return NotFound();

                        _repository.Delete(User);

                        if (await _repository.SaveChancesAsync())
                        {
                            return Ok();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                    }

                    return BadRequest();
                } */
    }
}