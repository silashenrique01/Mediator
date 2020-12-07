using System;


/// <summary>
/// TODO: Entidade que representa um usuario e as regras de negocio
/// </summary>
/// <param name="id"></param>
/// <returns></returns>

namespace Intuitive.Domain.Entities
{
    public class User
    {
        public User(){}
        
        public User(int _UserId, string _Name, DateTime _DtNasc, string _Email, string _Username, string _Password)
        {
            UserId = _UserId;
            Name = _Name;
            DtNasc = _DtNasc;
            Email = _Email;
            Username = _Username;
            Password = _Password;

        }

        

        public int UserId { get; private set; }
        public string Name { get; private set; }
        public DateTime DtNasc { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}