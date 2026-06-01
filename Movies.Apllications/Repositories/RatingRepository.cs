using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Repositories
{
    public class RatingRepository (RestDBContext _context): IRatingRepository
    {
        public async Task SaveAsyn(CancellationToken token)
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(Rating rating, CancellationToken token = default)
        {
            await _context.Ratings.AddAsync(rating, token);
        }
        public async Task<Rating?> GetUserRatingAsync(Guid movieId , string userId , CancellationToken token = default )
        {
            return await _context.Ratings
                   .SingleOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId , token);      

        }
       
        public async Task<float?> GetAverageRatingAsync(Guid movieId ,CancellationToken token)
        {
            if (movieId == Guid.Empty)
                throw new ArgumentException("movieId is required.", nameof(movieId));
              
            return await _context.Ratings
                .Where(r => r.MovieId == movieId)
                .Select(r => (float?)r.Score)
                .AverageAsync(token);
        }
        public async Task<(float? Average, int? UserRating)> GetAverageAndUserRatingAsync(
                                                                  Guid movieId,
                                                                  string? userId,
                                                                  CancellationToken token = default)
        {
            var avg = await GetAverageRatingAsync(movieId, token);
            int? userRating = null;

            if (userId is not null)
            {
                var rating = await GetUserRatingAsync(movieId, userId, token);
                userRating = rating?.Score;
            }

            return (avg, userRating);
        }
        public async Task DeleteRatingAsync(Guid movieId, string userId, CancellationToken token = default)
        {
            var rating = await _context.Ratings
                .SingleOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId, token);

            if (rating is null)
                return;

            _context.Ratings.Remove(rating);
            
        }
        public async Task<IEnumerable<MovieRating>> GetAllRatingAsync(string userId , CancellationToken token=default)
        {
            return await _context.Ratings
                .Where(x => x.UserId == userId)
                .Select(x => new MovieRating
                {
                    MovieId = x.MovieId,
                    Slug = x.Movie.Slug,
                    Rating = x.Score
                })
                .ToListAsync();
        }

    }
}
