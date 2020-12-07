using System;
using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita a edição de um usuario
/// </summary>
/// <param name=""></param>
/// <returns></returns>

namespace Intuitive.Domain.Commands
{
    public class UpdateUserCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime DtNasc { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
