using Intuitive.Domain.Commands.AuthenticationCommands;
using Intuitive.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using MediatR;
using Intuitive.Domain.Infra;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Intuitive.Domain.Handlers.Authentication
{
    public class RegisterHandler : IRequestHandler<RegisterCommands, Response>
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly SignInManager<UserApplication> _signInManager;

        public RegisterHandler(  UserManager<UserApplication> useManager,
                               SignInManager<UserApplication> signInManager)
        {
            _userManager = useManager;
            _signInManager = signInManager;
        }


        public async Task<Response> Handle(RegisterCommands request, CancellationToken cancellationToken)
        {
           Response response = new Response();
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == request.Username.ToUpper());

                    return response;
                }

                return response.AddError("Usuario Não Autorizado");
            }

            catch (SystemException ex)
            {
                return response.AddError($"Banco de Dados Falhou {ex.Message}");
            }

        }
    }
}

