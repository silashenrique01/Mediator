using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita um usuario específico pelo nome
/// </summary>
/// <param name="name"></param>
/// <returns></returns>

namespace Intuitive.Domain.Commands
{
    public class GetUserByNameCommand : IRequest<Response>
    {
        public GetUserByNameCommand(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
