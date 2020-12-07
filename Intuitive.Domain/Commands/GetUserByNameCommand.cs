using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita a lista de um usuario específico
/// </summary>
/// <param name="id"></param>
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
