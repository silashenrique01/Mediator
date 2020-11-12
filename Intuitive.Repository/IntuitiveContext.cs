using Microsoft.EntityFrameworkCore;
using Intuitive.Domain;

namespace Intuitive.Repository
{
    public class IntuitiveContext : DbContext
    {
        public IntuitiveContext(DbContextOptions<IntuitiveContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
    }
}
