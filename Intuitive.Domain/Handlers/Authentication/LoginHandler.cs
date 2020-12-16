using AutoMapper;
using Intuitive.Domain.Commands.AuthenticationCommands;
using Intuitive.Domain.Identity;
using Intuitive.Domain.Infra;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intuitive.Domain.Handlers.Authentication
{
    public class LoginHandler : IRequestHandler<LoginCommands, Response>
    {

        
        private readonly UserManager<UserApplication> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<UserApplication> _signInManager;

        public LoginHandler(IConfiguration config, UserManager<UserApplication> userManager, SignInManager<UserApplication> signInManager)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            
            
        }

        public async Task<Response> Handle(LoginCommands request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == request.Username.ToUpper());
                    

                    response.Token = GenerateJWToken(appUser).Result;
                    
                    return response;
                    
                }

                return response.AddError("Usuario Não Autorizado");
            }

            catch (SystemException ex)
            {
                return response.AddError($"Banco de Dados Falhou {ex.Message}");
            }
        }

        private async Task<string> GenerateJWToken(UserApplication user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
    }