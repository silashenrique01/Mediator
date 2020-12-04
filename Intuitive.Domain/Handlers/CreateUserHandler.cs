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
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IIntuitiveRepository _repository;

        public CreateUserHandler(IMediator mediator, IIntuitiveRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            User user = MapUserCommandToUserEntity(request);

            try
            {
                //aplica as regras de validação

                if (response.Success)
                {
                    _repository.Add(user);
                    if(await _repository.SaveChancesAsync())
                    response.SuccessMessage = "Usuario Cadastrado com sucesso"; //pegar texto de um arquivo de resource

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
                response.AddError(string.Format("Erro ao inserir novo Usuario: {0}", ex.ToString()));
            }

            return response;
        }

        private static User MapUserCommandToUserEntity(CreateUserCommand request)
        {
            return new User()
            {
                UserId = request.UserId,
                Name = request.Name,
                DtNasc = DateTime.Parse(request.DtNasc),
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            };
        }

    }
}
