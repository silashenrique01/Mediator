using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita a lista de todos os usuarios
/// </summary>
/// <param name="type"></param>
/// <returns></returns>

namespace Intuitive.Domain.Commands
{
    public class GetAllUserCommand : IRequest<Response>
    {
    }
}
