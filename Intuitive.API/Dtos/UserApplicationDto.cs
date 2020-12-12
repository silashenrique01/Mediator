using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intuitive.API.Dtos
{
    public class UserApplicationDto
    {
            
            [StringLength(100, MinimumLength = 3, ErrorMessage= "Nome deve ter entre 3 e 100 caracteres" )]
            public string FullName { get; set; }
            public string Email { get; set; }
            
            public string Username { get; set; }

            [Required(ErrorMessage="Campo Obrigatório")]
            public string Password { get; set; }

    }
}