using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita a criação de um usuario para autenticação
/// </summary>
/// <param name="type"></param>
/// <returns></returns>
/// 

namespace Intuitive.Domain.Commands.AuthenticationCommands
{
    public class RegisterCommands : IRequest<Response>
    {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
    }
}