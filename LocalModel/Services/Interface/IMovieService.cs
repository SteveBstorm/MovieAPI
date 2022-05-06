using LocalModel.Models;
using System.Collections.Generic;

namespace LocalModel.Services.Interface
{
    public interface IMovieService
    {
        int Create(NewMovie m);
        void Delete(int Id);
        IEnumerable<Actor> GetActors(int Id);
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> GetByRealisatorId(int Id);
        IEnumerable<Movie> GetByScenaristId(int Id);
        Movie GetOne(int Id);
        void SetAsActor(int MovieId, int PersonId, string Role);
        void Update(NewMovie m);
    }
}