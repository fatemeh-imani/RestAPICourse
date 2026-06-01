using Movies.Applications.DataBaces.Models;
using Movies.Applications.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Repositories
{
    public interface  IRatingRepository
    {
        Task<IEnumerable<MovieRating>> GetAllRatingAsync(string userId, CancellationToken token = default);
        Task DeleteRatingAsync(Guid movieId, string userId, CancellationToken token = default);
        Task<(float? Average, int? UserRating)> GetAverageAndUserRatingAsync(
                                                                Guid movieId,
                                                                string? userId,
                                                                CancellationToken token = default);
        Task<float?> GetAverageRatingAsync(Guid movieId, CancellationToken token);
        Task<Rating?> GetUserRatingAsync(Guid movieId, string userId, CancellationToken token = default);
        Task AddAsync(Rating rating, CancellationToken token = default);
        Task SaveAsyn(CancellationToken token);
    }
}
