using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Options
{
    public class GetAllMovieOption
    {
       
        public string? Title { get; init; }
        public int? YearOfRelease { get; init; }

        
        public string? SortBy { get; init; }
        public string? SortOrder { get; init; } // "asc" or "desc"

        
        public int Page { get; init; }
        public int PageSize { get; init; }

        public string? UserId { get; init; }
    }
}
