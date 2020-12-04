using MediatR;
using Intuitive.Domain.Commands;
using Intuitive.Domain.Infra;
using Intuitive.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intuitive.Domain.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IIntuitiveRepository _repository;

        public DeleteUserHandler(IMediator mediator, IIntuitiveRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                var user = await _repository.GetUsersAsyncById(request.UserId);   
                _repository.Delete(user);

                if (await _repository.SaveChancesAsync())
                    response.SuccessMessage = "User successfully deleted";

            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to search User with ID: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
