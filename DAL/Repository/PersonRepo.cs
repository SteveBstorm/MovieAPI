using DAL.Interface;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Repository
{
    public class PersonRepo : BaseRepository, IPersonRepository<Person, ActIn>
    {
        public PersonRepo(IConfiguration config) : base(config)
        {
        }

        public bool Delete(int Id)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText ="DeletePerson";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);

                    return cmd.ExecuteNonQuery() == 1;
                    
                }
            }
        }

        public IEnumerable<Person> GetAll()
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = "SELECT * FROM Person";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Person
                            {
                                Id = (int)reader["Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            };
                        }
                    }
                }
            }
        }

        public Person GetOne(int Id)
        {
            Person person = new Person();
            using (SqlConnection c = Connection())
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    c.Open();
                    cmd.CommandText = "SELECT * FROM Person WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            person.Id = (int)reader["Id"];
                            person.LastName = reader["LastName"].ToString();
                            person.FirstName = reader["FirstName"].ToString();
                        }
                    }
                }
            }
            return person;
        }

        public void Insert(Person p)
        {
            using(SqlConnection c = Connection())
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    c.Open();
                    cmd.CommandText = "INSERT INTO Person VALUES (@lastName, @firstName)";

                    cmd.Parameters.AddWithValue("firstName", p.FirstName);
                    cmd.Parameters.AddWithValue("lastName", p.LastName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Person p)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = "UPDATE Person SET LastName = @last, FirstName = @first WHERE Id = @id";
                    cmd.Parameters.AddWithValue("last", p.LastName);
                    cmd.Parameters.AddWithValue("first", p.FirstName);
                    cmd.Parameters.AddWithValue("id", p.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<ActIn> GetMovieByPersonId(int Id)
        {
            using (SqlConnection c = Connection())
            {
                c.Open();
                using (SqlCommand cmd = c.CreateCommand())
                {

                    cmd.CommandText = $"SELECT a.Role, m.Title FROM Movie m JOIN Actor a ON a.MovieID = m.Id WHERE a.PersonID = @id ";
                    cmd.Parameters.AddWithValue("id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new ActIn
                            {
                                Role = reader["Role"].ToString(),
                                MovieTitle = reader["Title"].ToString()
                            };
                        }
                    }
                }
            }
        }


    }
}
