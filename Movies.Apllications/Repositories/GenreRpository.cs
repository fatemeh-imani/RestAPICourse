using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Repositories
{
    public class GenreRpository(RestDBContext _context) : IGenreRepository
    {
        public async Task<IEnumerable<Genre>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            ids = ids.Distinct();

            return await _context.Genres
                .Where(g => ids.Contains(g.Id))
                   .ToListAsync();
        }
    }
}
