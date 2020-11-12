using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intuitive.API.Dtos
{
    public class UserDto{
            public int UserId { get; set; }     

            [Required(ErrorMessage="Campo Obrigatório")]
            [StringLength(100, MinimumLength = 3, ErrorMessage= "Nome deve ter entre 3 e 100 caracteres" )]
            public string Name { get; set; }
            public string DtNasc { get; set; }
            
            [EmailAddress]
            public string Email { get; set; }

            [StringLength(100, MinimumLength = 3, ErrorMessage= "Nome de usuário deve ter entre 3 e 100 caracteres" )]
            public string Username { get; set; }

            [Required(ErrorMessage="Campo Obrigatório")]
            public string Password { get; set; }

    }
}