using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita a exclusão de um usuario
/// </summary>
/// <param name="id"></param>
/// <returns></returns>

namespace Intuitive.Domain.Commands
{
    public class DeleteUserCommand : IRequest<Response>
    {
        public DeleteUserCommand(int id)
        {
            UserId = id;
        }
        public int UserId { get; }
    }
}
