using DAL.Interface;
using DAL.Repository;
using dal = DAL.Models;
using LocalModel.Models;
using LocalModel.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using LocalModel.Services.Interface;

namespace LocalModel.Services
{
    public class UserService : IUserService
    {
        IUserRepository<dal.User> _repo;
        public UserService(IUserRepository<dal.User> UserRepo)
        {
            _repo = UserRepo;
        }

        public IEnumerable<User> GetAll()
        {
            return _repo.GetAll().Select(x => x.toLocal());
        }

        public bool? CheckUser(User u)
        {
            bool? reponse = _repo.CheckUser(u.toDal());
            return reponse;
        }

        public User GetOne(int Id)
        {
            return _repo.GetOne(Id).toLocal();
        }

        public User GetByMail(string Email)
        {
            return _repo.GetByEmail(Email).toLocal();
        }

        public void Register(User user)
        {
            _repo.Insert(user.toDal());
        }

        public void Switchactivate(int Id)
        {
            _repo.Delete(Id);
        }

        public void Update(User u)
        {
            _repo.Update(u.toDal());
        }

        public void SwitchAdmin(int Id)
        {
            _repo.SetAdmin(Id);
        }


    }
}
