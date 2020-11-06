using DAL.Repository;
using local = LocalModel.Models;
using dal = DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using LocalModel.Tools;
using System.Linq;
using LocalModel.Models;
using DAL.Interface;
using LocalModel.Services.Interface;

namespace LocalModel.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository<dal.Movie, dal.Actor> _movieRepo;
        private IPersonRepository<dal.Person, dal.ActIn> _personRepo;

        public MovieService(IMovieRepository<dal.Movie, dal.Actor> movieRepo, IPersonRepository<dal.Person, dal.ActIn> PersonRepo)
        {
            _movieRepo = movieRepo;
            _personRepo = PersonRepo;
        }

        public void Delete(int Id)
        {
            _movieRepo.Delete(Id);
        }

        public void Update(MovieToDal m)
        {
            _movieRepo.Update(m.toDal());
        }

        public void Create(MovieToDal m)
        {
            _movieRepo.Insert(m.toDal());
        }

        public local.Movie GetOne(int Id)
        {
            return _movieRepo.GetOne(Id).toLocal(_movieRepo, _personRepo);
        }

        public IEnumerable<local.Movie> GetAll()
        {
            return _movieRepo.GetAll().Select(x => x.toLocal(_movieRepo, _personRepo));
        }

        public IEnumerable<local.Movie> GetByRealisatorId(int Id)
        {
           return _movieRepo.GetByRealisatorId(Id).Select(x => x.toLocal(_movieRepo, _personRepo));
        }

        public IEnumerable<local.Movie> GetByScenaristId(int Id)
        {
            return _movieRepo.GetByScenaristId(Id).Select(x => x.toLocal(_movieRepo, _personRepo));
        }

        public IEnumerable<local.Actor> GetActors(int Id)
        {
            return _movieRepo.GetActorsByFilmId(Id).Select(x => x.toLocal());
        }

        public void SetAsActor(int MovieId, int PersonId, string Role)
        {
            _movieRepo.SetAsActor(MovieId, PersonId, Role);
        }
    }
}
