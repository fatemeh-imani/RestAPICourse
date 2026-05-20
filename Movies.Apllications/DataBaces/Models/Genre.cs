using Movies.Applications.DataBaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.DataBaces.Models
{
    public class Genre :BaseModel
    {
     
        public string Name { get; set; } = default!;
        public ICollection<Movie> Movies { get; init; } = [];
    }
}
