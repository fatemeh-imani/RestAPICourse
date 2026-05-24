using Movies.Applications.DataBaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Services
{
    public interface IGenreRepository
    {
        public Task<IEnumerable<Genre>> GetByIdsAsync(IEnumerable<Guid> ids
                                                      , CancellationToken token = default);
    }
}
