using MediatR;
using Intuitive.Domain.Commands;
using Intuitive.Domain.Entities;
using Intuitive.Domain.Infra;
using Intuitive.Domain.Interfaces;
using Intuitive.Domain.Notifications;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intuitive.Domain.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IIntuitiveRepository _repository;

        public UpdateUserHandler(IMediator mediator, IIntuitiveRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            User user = new User(request.UserId, request.Name, request.DtNasc, request.Email, request.Username, request.Password);
            try
            {
            
                

                    
                _repository.Update(user);
                if (await _repository.SaveChancesAsync())
                {

                    response.SuccessMessage = "User successfully updated";

                    //notifica o sistema para enviar e-mail
                    await _mediator.Publish(new SendEmailNotification
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Username = user.Username

                    }, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to update User: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
