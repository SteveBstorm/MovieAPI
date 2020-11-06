using DAL.Interface;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Repository
{
    public class MovieRepo : BaseRepository, IMovieRepository<Movie, Actor>
    {
        public MovieRepo(IConfiguration config) : base(config)
        {
        }

        public void Delete(int Id)
        {
            using (SqlConnection c = Connection())
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    c.Open();
                    if (c.State == System.Data.ConnectionState.Open)
                    {
                        cmd.CommandText = "DELETE FROM Movie WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("Id", Id);

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                    else
                    {
                        throw new Exception("Erreur de connexion à la DB");
                    }
                }
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = "SELECT * FROM Movie";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Movie
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                ReleaseYear = (int)reader["ReleaseYear"],
                                RealisatorID = (int)reader["RealisatorID"],
                                ScenaristID = (int)reader["ScenaristID"],
                            };
                        }
                    }
                }
            }
        }

        public Movie GetOne(int Id)
        {
            Movie m = new Movie();
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = "SELECT * FROM Movie WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m.Id = (int)reader["Id"];
                            m.Title = reader["Title"].ToString();
                            m.Description = reader["Description"].ToString();
                            m.ReleaseYear = (int)reader["ReleaseYear"];
                            m.RealisatorID = (int)reader["RealisatorID"];
                            m.ScenaristID = (int)reader["ScenaristID"];

                        }
                    }
                }
                return m;
            }
        }

        public void Insert(Movie m)
        {
            using (SqlConnection c = Connection())
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    c.Open();
                    cmd.CommandText = "INSERT INTO Movie VALUES (@title, @description, @ReleaseYear, @RealisatorID, @ScenaristID)";
                    cmd.Parameters.AddWithValue("title", m.Title);
                    cmd.Parameters.AddWithValue("description", m.Description);
                    cmd.Parameters.AddWithValue("ReleaseYear", m.ReleaseYear);
                    cmd.Parameters.AddWithValue("RealisatorID", m.RealisatorID);
                    cmd.Parameters.AddWithValue("ScenaristID", m.ScenaristID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Movie m)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE Movie SET Title = @title, Description = @description, ReleaseYear = @releaseYear, RealisatorID = @real, ScenaristID = @scen WHERE Id = @id";
                    cmd.Parameters.AddWithValue("title", m.Title);
                    cmd.Parameters.AddWithValue("description", m.Description);
                    cmd.Parameters.AddWithValue("releaseYear", m.ReleaseYear);
                    cmd.Parameters.AddWithValue("real", m.RealisatorID);
                    cmd.Parameters.AddWithValue("scen", m.ScenaristID);
                    cmd.Parameters.AddWithValue("id", m.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public IEnumerable<Movie> GetByRealisatorId(int Id)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = $"SELECT * FROM Movie WHERE RealisatorID = @id";
                    cmd.Parameters.AddWithValue("id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Movie
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                ReleaseYear = (int)reader["ReleaseYear"],
                                RealisatorID = (int)reader["RealisatorID"],
                                ScenaristID = (int)reader["ScenaristID"],
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<Movie> GetByScenaristId(int Id)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = $"SELECT * FROM Movie WHERE ScenaristID = @id";
                    cmd.Parameters.AddWithValue("id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Movie
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                ReleaseYear = (int)reader["ReleaseYear"],
                                RealisatorID = (int)reader["RealisatorID"],
                                ScenaristID = (int)reader["ScenaristID"],
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<Actor> GetActorsByFilmId(int Id)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = $"SELECT a.Role, p.LastName, p.FirstName FROM Person p JOIN Actor a ON a.PersonID = p.Id WHERE a.MovieID = @id ";
                    cmd.Parameters.AddWithValue("id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Actor
                            {
                                Role = reader["Role"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                FirstName = reader["FirstName"].ToString()
                            };
                        }
                    }
                }
            }
        }

        public void SetAsActor(int MovieId, int PersonId, string Role)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Actor VALUES(@PersonID, @MovieID, @Role)";
                    cmd.Parameters.AddWithValue("PersonID", PersonId);
                    cmd.Parameters.AddWithValue("MovieID", MovieId);
                    cmd.Parameters.AddWithValue("Role", Role);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
