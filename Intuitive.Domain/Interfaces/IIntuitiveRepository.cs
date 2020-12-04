using System.Collections.Generic;
using System.Threading.Tasks;
using Intuitive.Domain.Entities;

namespace Intuitive.Domain.Interfaces
{
    public interface IIntuitiveRepository
    {
        //GERAL
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChancesAsync();

         //Users
         Task<User[]> GetAllUsersAsyncByName(string name);
         Task<User[]> GetAllUsersAsync();
         Task<User> GetUsersAsyncById(int UserId);
    }   
}