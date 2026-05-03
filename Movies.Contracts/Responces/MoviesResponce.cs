using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responces
{
    public class MoviesResponce
    {
        public required IEnumerable<MovieResponce> Items { get; init; } = Enumerable.Empty<MovieResponce>();
    }
}
