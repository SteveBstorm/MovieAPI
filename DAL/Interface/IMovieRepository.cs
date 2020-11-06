using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface IMovieRepository<Movie, Actor>
    {
        void Delete(int Id);
        IEnumerable<Actor> GetActorsByFilmId(int Id);
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> GetByRealisatorId(int Id);
        IEnumerable<Movie> GetByScenaristId(int Id);
        Movie GetOne(int Id);
        void Insert(Movie m);
        void SetAsActor(int MovieId, int PersonId, string Role);
        void Update(Movie m);
    }
}
