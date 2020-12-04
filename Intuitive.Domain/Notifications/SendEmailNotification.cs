using MediatR;
using System;

namespace Intuitive.Domain.Notifications
{
    public class SendEmailNotification : INotification
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime DtNasc { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
