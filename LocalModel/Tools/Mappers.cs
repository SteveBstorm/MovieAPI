using System;
using System.Collections.Generic;
using System.Text;

using local = LocalModel.Models;
using dal = DAL.Models;
using System.Runtime.CompilerServices;
using LocalModel.Services;
using DAL.Interface;
using DAL.Models;
using LocalModel.Services.Interface;
using System.Linq;

namespace LocalModel.Tools
{
    public static class Mappers
    {


        public static local.Movie toLocal(this dal.Movie m, IMovieRepository<dal.Movie, dal.Actor> _movieService, IPersonRepository<dal.Person, dal.ActIn> _personService)
        {
            return new local.Movie
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                Realisator = _personService.GetOne(m.RealisatorID).toLocal(),
                Scenarist = _personService.GetOne(m.ScenaristID).toLocal(),
                Actors = _movieService.GetActorsByFilmId(m.Id).Select(x => x.toLocal())
            };
        }

        public static local.Person toLocal(this dal.Person p)
        {
            return new local.Person
            {
                Id = p.Id,
                LastName = p.LastName,
                FirstName = p.FirstName
            };
        }

        public static dal.Person toDal(this local.Person p)
        {
            return new dal.Person
            {
                Id = p.Id,
                LastName = p.LastName,
                FirstName = p.FirstName
            };
        }

        public static local.CompletePerson toCPerson(this dal.Person p, IMovieRepository<dal.Movie, dal.Actor> _movieService, IPersonRepository<dal.Person, dal.ActIn> _personService)
        {
            return new local.CompletePerson
            {
                Id = p.Id,
                LastName = p.LastName,
                FirstName = p.FirstName,
                RealMovies = _movieService.GetByRealisatorId(p.Id).Select(x => x.toLocal(_movieService, _personService)),
                ScenMovies = _movieService.GetByScenaristId(p.Id).Select(x => x.toLocal(_movieService, _personService)),
                ActAs = _personService.GetMovieByPersonId(p.Id).Select(x => x.toLocal())
            };
        }

    

        public static dal.Movie toDal(this local.MovieToDal m)
        {
            return new dal.Movie
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                RealisatorID = m.RealisatorID,
                ScenaristID = m.ScenaristID
            };
        }

        public static local.Actor toLocal(this dal.Actor a)
        {
            return new local.Actor
            {
                Id = a.Id,   
                Role = a.Role,
                LastName = a.LastName,
                FirstName = a.FirstName
            };
        }

        public static local.ActIn toLocal(this dal.ActIn a)
        {
            return new local.ActIn
            {
                Role = a.Role,
                MovieTitle = a.MovieTitle,
                MovieID = a.MovieId
            };
        }

        public static local.User toLocal(this dal.User u)
        {
            return new local.User
            {
                Id = u.Id,
                Email = u.Email,
                Password = u.Password,
                LastName = u.LastName,
                FirstName = u.FirstName,
                BirthDate = u.BirthDate,
                IsActive = u.IsActive,
                IsAdmin = u.IsAdmin
            };
        }

        public static dal.User toDal(this local.User u)
        {
            return new dal.User
            {
                Id = u.Id,
                Email = u.Email,
                Password = u.Password,
                LastName = u.LastName,
                FirstName = u.FirstName,
                BirthDate = u.BirthDate,
                IsActive = u.IsActive,
                IsAdmin = u.IsAdmin
            };
        }

        public static local.Comment toLocal(this dal.Comment c)
        {
            return new local.Comment
            {
                Id = c.Id,
                Content = c.Content,
                PostDate = c.PostDate,
                MovieID = c.MovieID,
                UserID = c.UserID
            };
        }
        public static dal.Comment toDal(this local.Comment c)
        {
            return new dal.Comment
            {
                Id = c.Id,
                Content = c.Content,
                PostDate = c.PostDate,
                MovieID = c.MovieID,
                UserID = c.UserID
            };
        }
    }
}
