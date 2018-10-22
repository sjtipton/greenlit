using greenlit.Entities;
using System;
using System.Collections.Generic;

namespace greenlit.Services
{
    public interface IUserService
    {
        User Authenticate(string identifier, string password);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(Guid id);
    }
}
