using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.Options;
using Movies.Applications.Projections;

namespace Movies.Applications.MovieRepositories
{
    public class MovieRepository(RestDBContext _context) : IMovieRepository
    {
        public async Task SaveAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }

        public async Task CreateAsync(Movie movie, CancellationToken token = default)
        {
            await _context.Movies.AddAsync(movie, token);

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
        public async Task<IEnumerable<MovieReadModel>> GetAllAsync( GetAllMovieOption options, CancellationToken token = default)
        {
            var query = _context.Movies.AsNoTracking();
            //این داده‌ها فقط برای خواندن هستند، لازم نیست داخل Change Tracker نگه‌شان داری.

            
            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                query = query.Where(m => m.Title.Contains(options.Title));
            }
            if (options.YearOfRelease.HasValue)
            {
                query = query.Where(m => m.YearOfRelease == options.YearOfRelease.Value);
            }

            query = options.SortBy?.ToLowerInvariant() switch
            {
                "title" => options.SortOrder == "desc"
                    ? query.OrderByDescending(m => m.Title)
                    : query.OrderBy(m => m.Title),
                "yearofrelease" => options.SortOrder == "desc"
                    ? query.OrderByDescending(m => m.YearOfRelease)
                    : query.OrderBy(m => m.YearOfRelease),
                _ => query.OrderBy(m => m.Id) // سورت پیش‌فرض
            };

          
            // فرمول: (PageNumber - 1) * PageSize
            query = query
                .Skip((options.Page - 1) * options.PageSize)
                .Take(options.PageSize);

            return await query
                .AsSplitQuery()
                .Select(m => new MovieReadModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Slug = m.Slug,
                    YearOfRelease = m.YearOfRelease,
                    Genres = m.Genres.Select(g => g.Name).ToList(),
                    Rating = m.Ratings.Select(r => (float?)r.Score).Average(),
                    UserRating = options.UserId == null ? null : m.Ratings
                        .Where(r => r.UserId == options.UserId)
                        .Select(r => (int?)r.Score)
                        .FirstOrDefault()
                })
                .ToListAsync(token);


        }


    }
}
