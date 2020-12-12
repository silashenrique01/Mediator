using Intuitive.Domain.Entities;
using Intuitive.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intuitive.Repository
{
    public class IntuitiveContext : IdentityDbContext<UserApplication, Role, int,
                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public IntuitiveContext(DbContextOptions<IntuitiveContext> options) : base(options){}

        public DbSet<User> UsersIntuitive {get; set;}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>( userRole =>{
            
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});
                
                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId).IsRequired();
                
                
                userRole.HasOne(ur => ur.UserApplication)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId).IsRequired();

            });

            
        }

    }
}
