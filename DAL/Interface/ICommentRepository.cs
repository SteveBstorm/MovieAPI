using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface ICommentRepository<Comment>
    {
        IEnumerable<Comment> GetAll();
        Comment GetOne(int Id);
        void Insert(Comment u);
        void Update(Comment u);
        bool Delete(int Id);
        IEnumerable<Comment> GetByMovieId(int Id);
    }
}
