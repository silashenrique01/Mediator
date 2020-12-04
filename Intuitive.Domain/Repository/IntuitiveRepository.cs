using System.Linq;
using System.Threading.Tasks;
using Intuitive.Domain.Interfaces;
using Intuitive.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intuitive.Domain.Repository
{
    public class IntuitiveRepository : IIntuitiveRepository
    {
        private readonly IntuitiveContext _context;
        public IntuitiveRepository(IntuitiveContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChancesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //User
        public async Task<User[]> GetAllUsersAsync()
        {
            IQueryable<User> query = _context.Users;

            query = query.AsNoTracking()
                        .OrderBy(c => c.UserId);

            return await query.ToArrayAsync();
        }
        public async Task<User[]> GetAllUsersAsyncByName(string name)
        {
            IQueryable<User> query = _context.Users;

            query = query.AsNoTracking()
                        .OrderBy(c => c.Name)
                        .Where(c => c.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }


        public async Task<User> GetUsersAsyncById(int id)
        {
             IQueryable<User> query = _context.Users;

            query = query.AsNoTracking()
                        .OrderBy(c => c.UserId)
                        .Where(c => c.UserId == id);
                        
            return await query.FirstOrDefaultAsync();
        }
    }
}