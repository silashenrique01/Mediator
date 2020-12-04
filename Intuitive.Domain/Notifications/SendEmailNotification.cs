using MediatR;

namespace Intuitive.Domain.Notifications
{
    public class SendEmailNotification : INotification
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
