using System;
using Intuitive.Domain.Infra;
using MediatR;

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
