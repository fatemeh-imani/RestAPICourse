using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Requests
{
    public class GetAllMovieRequest
    {
        public string? Title { get; init; }
        public int? YearOfRelease { get; init; }

        public string? SortBy { get; init; }
        public string? SortOrder { get; init; }

        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;

    }
}
