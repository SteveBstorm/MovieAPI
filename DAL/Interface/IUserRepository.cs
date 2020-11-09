using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface IUserRepository<User>
    {
        IEnumerable<User> GetAll();
        User GetOne(int Id);
        void Insert(User u);
        void Update(User u);
        bool Delete(int Id);
        bool? CheckUser(User u);
        User GetByEmail(string email);
        void SetAdmin(int Id);
    }
}

