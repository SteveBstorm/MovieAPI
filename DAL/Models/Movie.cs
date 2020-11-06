using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int RealisatorID { get; set; }
        public int ScenaristID { get; set; }
    }
}
