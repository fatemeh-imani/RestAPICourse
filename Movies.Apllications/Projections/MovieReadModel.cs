using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Projections
{
    public sealed class MovieReadModel
    {
            public Guid Id { get; init; }
            public string Title { get; init; } = default!;
            public string Slug { get; init; } = default!;
            public int YearOfRelease { get; init; }
            public float? Rating { get; init; }
            public int? UserRating { get; init; }
            public IReadOnlyList<string> Genres { get; init; } = [];
           

    }
}
