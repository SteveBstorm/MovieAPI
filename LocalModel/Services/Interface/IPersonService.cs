using LocalModel.Models;
using System.Collections.Generic;

namespace LocalModel.Services.Interface
{
    public interface IPersonService
    {
        void Create(Person p);
        bool Delete(int Id);
        IEnumerable<ActIn> GetActs(int Id);
        IEnumerable<Person> GetAll();
        CompletePerson GetComplete(int Id);
        Person GetOne(int Id);
        void Update(Person p);
    }
}