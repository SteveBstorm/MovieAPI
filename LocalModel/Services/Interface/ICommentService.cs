using System;
using System.Collections.Generic;
using System.Text;

namespace LocalModel.Services.Interface
{
    public interface ICommentService<Comment>
    {
        IEnumerable<Comment> GetAll();
        Comment GetOne(int Id);
        void Insert(Comment u);
        void Update(Comment u);
        bool Delete(int Id);
        IEnumerable<Comment> GetByMovieId(int Id);
    }
}
