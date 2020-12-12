using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Intuitive.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }     
    }
}