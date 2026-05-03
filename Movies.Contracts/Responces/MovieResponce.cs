using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responces
{
    public class MovieResponce
    {
        public required Guid Id { get; set; }
        public required string Title { get; init; }
        public required string Slug  { get; init; }
        public required int YearOfRelease { get; init; }
        public required IEnumerable<string> Genres { get; init; } = Enumerable.Empty<string>();

    }
}
