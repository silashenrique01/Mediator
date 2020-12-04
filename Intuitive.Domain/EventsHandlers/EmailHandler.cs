using Intuitive.Domain.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intuitive.Domain.EventsHandlers
{
    public class EmailHandler : INotificationHandler<SendEmailNotification>
    {
        public Task Handle(SendEmailNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => Console.WriteLine("Send Email - The user {0} was successfully registered", notification.Name));
        }
    }
}
