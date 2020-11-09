using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBApi.Models
{
    public class Actor
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public string Role { get; set; }
    }
}
