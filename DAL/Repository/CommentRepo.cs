using ADOToolbox;
using DAL.Interface;
using DAL.Models;
using DAL.Tools;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public class CommentRepo : BaseRepository, ICommentRepository<Comment>
    {
        public CommentRepo(IConfiguration config) : base(config)
        {

        }

        public bool Delete(int Id)
        {
            Command cmd = new Command("DELETE FROM Comment WHERE Id = @Id");
            cmd.AddParameter("Id", Id);
            return _connection.ExecuteNonQuery(cmd) == 1;
        }

        public IEnumerable<Comment> GetAll()
        {
            Command cmd = new Command("SELECT * FROM Comment");
            return _connection.ExecuteReader(cmd, Converters.CommentConvert);
        }

        public Comment GetOne(int Id)
        {
            Command cmd = new Command("SELECT * FROM Comment WHERE Id = @Id");
            cmd.AddParameter("Id", Id);
            return _connection.ExecuteReader(cmd, Converters.CommentConvert).FirstOrDefault();
        }

        public void Insert(Comment u)
        {
            string Query = "INSERT INTO Comment (Content, PostDate, UserID, MovieID) VALUES(@Content, @Post, @userID, @movieID)";
            Command cmd = new Command(Query);
            cmd.AddParameter("Content", u.Content);
            cmd.AddParameter("Post", u.PostDate);
            cmd.AddParameter("userID", u.UserID);
            cmd.AddParameter("movieID", u.MovieID);

            _connection.ExecuteNonQuery(cmd);
        }

        public void Update(Comment u)
        {
            string Query = "UPDATE Comment SET [Content] = @Content, PostDate = @Post, UserID = @userID, MovieID = @movieID WHERE Id = @Id";
            Command cmd = new Command(Query);
            cmd.AddParameter("Content", u.Content);
            cmd.AddParameter("Post", u.PostDate);
            cmd.AddParameter("userID", u.UserID);
            cmd.AddParameter("movieID", u.MovieID);
            cmd.AddParameter("Id", u.Id);

            _connection.ExecuteNonQuery(cmd);
        }

        public IEnumerable<Comment> GetByMovieId(int Id)
        {
            string Query = "SELECT * FROM Comment WHERE MovieID = @Id";
            Command cmd = new Command(Query);
            cmd.AddParameter("Id", Id);

            return _connection.ExecuteReader<Comment>(cmd);
        }
    }
}
