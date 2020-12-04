using Intuitive.Domain.Infra;
using MediatR;

namespace Intuitive.Domain.Commands
{
    public class GetUserByIdCommand : IRequest<Response>
    {
        public GetUserByIdCommand(int id)
        {
            UserId = id;
        }
        public int UserId { get; }
    }
}
