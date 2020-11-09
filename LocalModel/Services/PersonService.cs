using DAL.Repository;
using local = LocalModel.Models;
using dal = DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using LocalModel.Tools;
using System.Linq;
using DAL.Models;
using DAL.Interface;
using LocalModel.Services.Interface;

namespace LocalModel.Services
{
    public class PersonService : IPersonService
    {
        private IMovieRepository<dal.Movie, dal.Actor> _movieRepo;
        private IPersonRepository<dal.Person, dal.ActIn> _repo;
        public PersonService(IPersonRepository<dal.Person, dal.ActIn> PersonRepo, IMovieRepository<dal.Movie, dal.Actor> movieRepository)
        {
            _repo = PersonRepo;
            _movieRepo = movieRepository;
        }

        public local.Person GetOne(int Id)
        {
            return _repo.GetOne(Id).toLocal();
        }

        public local.CompletePerson GetComplete(int Id)
        {

            return _repo.GetOne(Id).toCPerson(_movieRepo, _repo);
        }

        public IEnumerable<local.Person> GetAll()
        {
            return _repo.GetAll().Select(x => x.toLocal());
        }

        public void Create(local.Person p)
        {
            _repo.Insert(p.toDal());
        }

        public IEnumerable<local.ActIn> GetActs(int Id)
        {
            return _repo.GetMovieByPersonId(Id).Select(x => x.toLocal());
        }

        public bool Delete(int Id)
        {
            return _repo.Delete(Id);
        }

        public void Update(local.Person p)
        {
            _repo.Update(p.toDal());
        }

        public void SetAsActor(int movieId, int personId, string Role)
        {
            _repo.SetAsActor(movieId, personId, Role);
        }

    }
}
