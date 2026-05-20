using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Models;

namespace Movies.Applications.MovieRepositories
{
    public class MovieRepository(RestDBContext _context) : IMovieRepository
    {
        public async Task  SaveAsyn()
        {
           await _context.SaveChangesAsync();
        }
        
        public async Task  CreateAsync(Movie movie)
        {
          await  _context.Movies.AddAsync(movie);
            
        }
        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await _context.Movies
                .Include(x => x.Genres)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie?> GetBySlugAsync(string slug)
        {
          return await  _context.Movies
                .Include (x => x.Genres)
                .SingleOrDefaultAsync(x => x.Slug == slug);
        }
      
        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                                 .Include(m => m.Genres)
                                 .ToListAsync();
        }

       

    
    }
}
