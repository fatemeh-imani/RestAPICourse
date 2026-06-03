using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Pagination
{
    public class PagedResponse<T>
    {
        public required IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
        public required int Page { get; init; }
        public required int PageSize { get; init; }
        public required int TotalCount { get; init; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }
}
