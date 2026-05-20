using Movies.Applications.DataBaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Applications.DataBaces.Models
{
    public partial class Movie : BaseModel
    {
       
        public string Title { get; set; } = default!;
        public  string Slug {  get; set; }= default!;
        public  int YearOfRelease { get; set; }
        public ICollection<Genre> Genres { get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];



    }
}
