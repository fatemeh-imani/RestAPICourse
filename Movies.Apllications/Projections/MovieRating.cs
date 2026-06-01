using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Projections
{
    public class MovieRating
    {
        public Guid MovieId { get; init; }
        public string Slug { get; init; } = default!;
        public int Rating { get; init; }

    }
}
