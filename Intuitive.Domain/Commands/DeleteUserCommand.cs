using Intuitive.Domain.Infra;
using MediatR;

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
