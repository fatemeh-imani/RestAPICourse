using FluentValidation;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.Projections;
using Movies.Applications.Repositories;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;

namespace Movies.Applications.Services
{
    public class RatingService (
        IRatingRepository _ratingRepository
        ,IValidator<CreateRatingRequest> _creatValidator
        ) : IRatingService
    {
        
        public async Task RateMovieAsync(Guid movieId 
             , string userId , CreateRatingRequest ratingRequest , CancellationToken token=default)
        {
            if (movieId == Guid.Empty)
                throw new ArgumentException("movieId is required.", nameof(movieId));

            await _creatValidator.ValidateAndThrowAsync(ratingRequest, token);


           var rating = await _ratingRepository.GetUserRatingAsync(movieId , userId , token);

            if(rating is not  null)
            {
                rating.Score = ratingRequest.rating;
            }
            else
            {
                rating = new  Rating
                {
                    MovieId = movieId,
                    UserId = userId ,
                    Score = ratingRequest.rating
                };
                await _ratingRepository.AddAsync(rating, token);

            }

            await _ratingRepository.SaveAsyn(token);
        }

        public async Task DeleteRatingAsync(Guid movieId, string userId, CancellationToken token = default)
        {
            if (movieId == Guid.Empty)
                throw new ArgumentException("movieId is required.", nameof(movieId));

            await _ratingRepository.DeleteRatingAsync(movieId , userId , token);
           await _ratingRepository.SaveAsyn(token);
        }
        public async Task<IEnumerable<RatingResponce>> GetAllRatingAsync(string userId, CancellationToken token = default)
        {
            var ratings = await _ratingRepository.GetAllRatingAsync(userId, token);

            return ratings.Select(x => new RatingResponce
            {
                MovieId = x.MovieId,
                Slug = x.Slug,
                Rating = x.Rating
            });
        }

    }


}
