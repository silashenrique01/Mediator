using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Intuitive.API.Dtos;
using Intuitive.Domain;
using Intuitive.Repository;
using AutoMapper;

namespace Intuitive.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IIntuitiveRepository _repository;
        private readonly IMapper _mapper;
        public UserController(IIntuitiveRepository repository, IMapper mapper)
        {

            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _repository.GetAllUsersAsync();

                var results = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(users);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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

        [HttpPost]
        public async Task<IActionResult> Post(UserDto model)
        {
            try
            {
                //Mapeamento inverso -> Dto para User
                var user = _mapper.Map<User>(model);

                _repository.Add(user);

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
        }
    }
}