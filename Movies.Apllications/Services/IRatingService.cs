using Movies.Contracts.Requests;
using Movies.Contracts.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Services
{
    public interface IRatingService
    {
        Task RateMovieAsync(Guid movieId, string userId, CreateRatingRequest ratingRequest, CancellationToken token = default);

        Task DeleteRatingAsync(Guid movieId, string userId, CancellationToken token = default);

        Task<IEnumerable<RatingResponce>> GetAllRatingAsync(string userId, CancellationToken token = default);


    }
}
