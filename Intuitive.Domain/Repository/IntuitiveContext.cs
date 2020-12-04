using Microsoft.EntityFrameworkCore;
using Intuitive.Domain.Entities;

namespace Intuitive.Domain.Repository
{
    public class IntuitiveContext : DbContext
    {
        public IntuitiveContext(DbContextOptions<IntuitiveContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
    }
}
