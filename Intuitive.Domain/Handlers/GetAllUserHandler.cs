using Intuitive.Domain.Interfaces;
using MediatR;
using Intuitive.Domain.Commands;
using Intuitive.Domain.Infra;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intuitive.Domain.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUserCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IIntuitiveRepository _repository;

        public GetAllUsersHandler(IMediator mediator, IIntuitiveRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Response> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                response.Content = await _repository.GetAllUsersAsync();
                response.SuccessMessage = "List of Users successfully recovered";
            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to retrive all Users: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
