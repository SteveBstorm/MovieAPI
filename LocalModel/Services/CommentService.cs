using DAL.Interface;
using LocalModel.Models;
using LocalModel.Services.Interface;
using LocalModel.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
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
           return _repo.Delete(Id);
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetByMovieId(int Id)
        {
            return _repo.GetByMovieId(Id).Select(x => x.toLocal());
        }

        public Comment GetOne(int Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Comment u)
        {
            _repo.Insert(u.toDal());
        }

        public void Update(Comment u)
        {
            throw new NotImplementedException();
        }
    }
}
