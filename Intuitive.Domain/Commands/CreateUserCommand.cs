using System;
using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita a criação de um usuario
/// </summary>
/// <param name="type"></param>
/// <returns></returns>

namespace Intuitive.Domain.Commands
{
    public class CreateUserCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime DtNasc { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


    }
}