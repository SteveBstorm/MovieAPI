using LocalModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBApi.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Person Realisator { get; set; }
        public Person Scenarist { get; set; }

        public TestModel(Person real, Person scen, int Id, string Title)
        {
            this.Id = Id;
            this.Title = Title;
            Realisator = real;
            Scenarist = scen;
        }
    }


}
