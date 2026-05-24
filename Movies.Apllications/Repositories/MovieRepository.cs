using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Models;

namespace Movies.Applications.MovieRepositories
{
    public class MovieRepository(RestDBContext _context) : IMovieRepository
    {
        public async Task  SaveAsync(CancellationToken token = default)
        {
           await _context.SaveChangesAsync(token);
        }
        
        public async Task  CreateAsync(Movie movie, CancellationToken token = default)
        {
          await  _context.Movies.AddAsync(movie , token);
            
        }
        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _context.Movies
                .Include(x => x.Genres)
                .SingleOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<Movie?> GetBySlugAsync(string slug, CancellationToken token = default)
        {
          return await  _context.Movies
                .Include (x => x.Genres)
                .SingleOrDefaultAsync(x => x.Slug == slug, token);
        }
      
        public async Task<IEnumerable<Movie>> GetAllAsync( CancellationToken token = default)
        {
            return await _context.Movies
                                 .Include(m => m.Genres)
                                 .ToListAsync(token);
        }

       

    
    }
}
