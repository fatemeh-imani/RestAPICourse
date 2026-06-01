using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.Projections;

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
            return await _context.Movies
                  .Include(x => x.Genres)
                  .SingleOrDefaultAsync(x => x.Slug == slug, token);
        }
         public async Task<IEnumerable<MovieReadModel>> GetAllAsync(string? userId=null , CancellationToken token = default)
        {
            return await _context.Movies
          .AsNoTracking()
          .OrderBy(m => m.Title)
          .Select(m => new MovieReadModel
          {
              Id = m.Id,
              Title = m.Title,
              Slug = m.Slug,
              YearOfRelease = m.YearOfRelease,

              Genres = m.Genres
                  .Select(g => g.Name)
                  .ToList(),

              Rating = m.Ratings
                  .Select(r => (float?)r.Score)
                  .Average(),

           
              UserRating = userId == null
                  ? null
                  : m.Ratings
                      .Where(r => r.UserId == userId)
                      .Select(r => (int?)r.Score)
                      .FirstOrDefault()
          })
          .ToListAsync(token);
        }

    
    }
}
