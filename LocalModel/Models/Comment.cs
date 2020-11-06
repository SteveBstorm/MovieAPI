using System;
using System.Collections.Generic;
using System.Text;

namespace LocalModel.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public int UserID { get; set; }
        public int MovieID { get; set; }
    }
}
