using MediatR;
using Intuitive.Domain.Commands;
using Intuitive.Domain.Infra;
using Intuitive.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intuitive.Domain.Handlers
{
    public class GetUserByNameHandler : IRequestHandler<GetUserByNameCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IIntuitiveRepository _repository;

        public GetUserByNameHandler(IMediator mediator, IIntuitiveRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Response> Handle(GetUserByNameCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                response.Content = await _repository.GetAllUsersAsyncByName(request.Name);

                if (response.Content != null)
                    response.SuccessMessage = "User successfully recovered";

            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to search User with Name: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
