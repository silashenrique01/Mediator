﻿using MediatR;
using Intuitive.Domain.Commands;
using Intuitive.Domain.Infra;
using Intuitive.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Intuitive.Domain.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IIntuitiveRepository _repository;

        public GetUserByIdHandler(IMediator mediator, IIntuitiveRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Response> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                response.Content = await _repository.GetUsersAsyncById(request.UserId);

                if (response.Content != null)
                    response.SuccessMessage = "User successfully recovered";

            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to search User with ID: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
