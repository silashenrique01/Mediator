using System.Collections.Generic;
using System.Threading.Tasks;
using Intuitive.Domain;

namespace Intuitive.Repository
{
    public interface IIntuitiveRepository
    {
        //GERAL
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChancesAsync();

         //Users
         Task<User[]> GetAllUsersAsyncByName(string nome);
         Task<User[]> GetAllUsersAsync();
         Task<User> GetUsersAsyncById(int userId);
    }   
}