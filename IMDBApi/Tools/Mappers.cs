using IMDBApi.Models;
using LocalModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBApi.Tools
{
    public static class Mappers
    {
        public static User toLocal(this NewUserInfo newUser)
        {
            return new User
            {
                Email = newUser.Email,
                LastName = newUser.LastName,
                FirstName = newUser.FirstName,
                Password = newUser.Password,
                BirthDate = newUser.BirthDate
            };
        }

        public static LocalModel.Models.Person toLocal(this Models.Person p)
        {
            return new LocalModel.Models.Person
            {
                LastName = p.LastName,
                FirstName = p.FirstName
            };
        }
    }
}
