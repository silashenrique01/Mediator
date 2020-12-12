using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Intuitive.Domain.Identity
{
    public class UserApplication : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public List<UserRole> UserRoles { get; set; }  
    }
}