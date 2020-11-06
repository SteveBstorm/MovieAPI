using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface IPersonRepository<Person, ActIn>
    {
        IEnumerable<Person> GetAll();
        Person GetOne(int Id);
        void Insert(Person c);
        void Update(Person c);
        bool Delete(int Id);
        IEnumerable<ActIn> GetMovieByPersonId(int Id);
    }
}
