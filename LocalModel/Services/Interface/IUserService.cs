using LocalModel.Models;
using System.Collections.Generic;

namespace LocalModel.Services.Interface
{
    public interface IUserService
    {
        bool? CheckUser(User u);
        IEnumerable<User> GetAll();
        User GetByMail(string Email);
        User GetOne(int Id);
        void Register(User user);
        void Switchactivate(int Id);
        void Update(User u);
        void SwitchAdmin(int Id);
    }
}