using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita o login de um usuario para acessar a API
/// </summary>
/// <returns></returns>
/// 

namespace Intuitive.Domain.Commands.AuthenticationCommands
{
    public class LoginCommands : IRequest<Response>
    {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
    }
}