using System;
using System.Collections.Generic;
using System.Text;

namespace LocalModel.Models
{
    public class CompletePerson
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public IEnumerable<Movie> RealMovies { get; set; }
        public IEnumerable<Movie> ScenMovies { get; set; }
        
        public IEnumerable<ActIn> ActAs { get; set; }

    }
}
