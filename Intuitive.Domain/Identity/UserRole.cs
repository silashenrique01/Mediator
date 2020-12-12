using Microsoft.AspNetCore.Identity;

namespace Intuitive.Domain.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public UserApplication UserApplication { get; set; }

        public Role Role { get; set; }
    }
}