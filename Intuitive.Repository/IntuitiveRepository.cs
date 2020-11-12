using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Intuitive.Domain;
using System.Security.Cryptography;
using System.Text;

namespace Intuitive.Repository
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

        public string GerarHashMd5(string password)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public async Task<User[]> GetAllUsersAsyncByName(string name)
        {
            IQueryable<User> query = _context.Users;

            query = query.AsNoTracking()
                        .OrderBy(c => c.Name)
                        .Where(c => c.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<User> GetUsersAsyncById(int UserId)
        {
            IQueryable<User> query = _context.Users;

            query = query.AsNoTracking()
                        .OrderByDescending(c => c.UserId)
                        .Where(c => c.UserId == UserId);

            return await query.FirstOrDefaultAsync();
        }
    }
}