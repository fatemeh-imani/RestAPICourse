using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.DTO
{
    public class MovieDTO
    {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public int YearOfRelease { get; set; }
            public List<string> Genres { get; set; }
            public string Slug { get; set; }
    }
}
