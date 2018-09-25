using greenlit.Entities;
using System.Collections.Generic;

namespace greenlit.Services
{
    public interface IUserService
    {
        User Authenticate(string identifier, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}
