using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Tools
{
    public static class Converters
    {
        public static User Convert(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Email = reader["Email"].ToString(),
                Password = reader["Password"].ToString(),
                FirstName = reader["FisrtName"].ToString(),
                LastName = reader["LastName"].ToString(),
                BirthDate = (DateTime)reader["BirthDate"],
                IsActive = (bool)reader["IsActive"],
                IsAdmin = (bool)reader["IsAdmin"]
            };
        }

        public static Comment CommentConvert(SqlDataReader reader)
        {
            return new Comment
            {
                Id = (int)reader["Id"],
                Content = reader["Content"].ToString(),
                PostDate = (DateTime)reader["PostDate"],
                UserID = (int)reader["UserID"],
                MovieID = (int)reader["MovieID"]
            };
        }
    }
}
