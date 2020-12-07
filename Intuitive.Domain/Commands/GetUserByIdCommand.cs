﻿using Intuitive.Domain.Infra;
using MediatR;

/// <summary>
/// TODO: Comando que requisita a lista de um usuario específico
/// </summary>
/// <param name="id"></param>
/// <returns></returns>

namespace Intuitive.Domain.Commands
{
    public class GetUserByIdCommand : IRequest<Response>
    {
        public GetUserByIdCommand(int id)
        {
            UserId = id;
        }
        public int UserId { get; }
    }
}
