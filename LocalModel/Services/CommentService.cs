using DAL.Interface;
using LocalModel.Models;
using LocalModel.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using dal = DAL.Models;

namespace LocalModel.Services
{
    public class CommentService : ICommentService<Comment>
    {
        private ICommentRepository<dal.Comment> _repo;

        public CommentService(ICommentRepository<dal.Comment> commentRepository)
        {
            _repo = commentRepository;
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetOne(int Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Comment u)
        {
            throw new NotImplementedException();
        }

        public void Update(Comment u)
        {
            throw new NotImplementedException();
        }
    }
}
