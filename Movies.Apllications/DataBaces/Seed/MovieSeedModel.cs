using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ovies.Applications.DataBaces.Seed
{
    public class MovieSeedModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Slug { get; set; } = default!;

        public int YearOfRelease { get; set; }

        public List<string> Genres { get; set; } = [];

        public List<string> Links { get; set; } = [];
    }
}
