using FluentValidation;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.MovieRepositories;
using Movies.Applications.Options;
using Movies.Applications.Pagination;
using Movies.Applications.Repositories;
using Movies.Applications.Utilities;
using Movies.Applications.Validator;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;

namespace Movies.Applications.Services
{
    public class MovieService(
        IMovieRepository _movieRepository,
        IGenreRepository _genreRepository,
        IRatingRepository _ratingRepository,
        IValidator<CreateMovieRequste> _createValidator,
         IValidator<UpdateMovieRequste> _updateValidator,
         IValidator<GetAllMovieRequest> _getAllRequstValidator
        ) : IMovieService
    {
        public async Task<MovieResponce> CreateAsync(CreateMovieRequste movieRequest
                                                      ,CancellationToken token = default)
        {
            await _createValidator.ValidateAndThrowAsync( movieRequest , token);

            var slug = SlugGenerator.Generate(movieRequest.Title, movieRequest.YearOfRelease);

            var existingMovie = await _movieRepository.GetBySlugAsync(slug , token);

            if (existingMovie != null)
                throw new Exception("Movie with same slug already exists");

            var movie = new Movie
            {
                Title = movieRequest.Title,
                YearOfRelease = movieRequest.YearOfRelease,
                Slug = slug

            };

            if (movieRequest.GenreIds?.Any() == true)
            {
                var genres = await _genreRepository.GetByIdsAsync(movieRequest.GenreIds , token);
                movie.Genres = genres.ToList();
            }

            await _movieRepository.CreateAsync(movie , token);

              await  _movieRepository.SaveAsync(token);


            return new MovieResponce
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Slug = movie.Slug,
                Genres = movie.Genres.Select(g => g.Name)
            };
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken token = default)
        {
           var movie = await _movieRepository.GetByIdAsync(id , token);

            if (movie is null) return  false;

            movie.IsDeleted = true;

              await _movieRepository.SaveAsync(token);
            return true;     
        }
       
        public async Task<PagedResponse<MovieResponce>> GetAllAsync(GetAllMovieRequest request,
                                                                  string userId,
                                                                   CancellationToken token = default)
        {
            await _getAllRequstValidator.ValidateAndThrowAsync(request , token);

            var options = new GetAllMovieOption
            {
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                SortBy = request.SortBy,
                SortOrder = request.SortOrder,
                Page = request.Page,
                PageSize = request.PageSize,
                UserId = userId
            };

            var movies = await _movieRepository.GetAllAsync( options  , token);


            return new PagedResponse<MovieResponce>
            {
                Items = movies.Items.Select(movie => new MovieResponce
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    YearOfRelease = movie.YearOfRelease,
                    Slug = movie.Slug,
                    Genres = movie.Genres,
                    Rating = movie.Rating,
                    UserRating = movie.UserRating
                }).ToList(),
                Page = movies.Page,
                PageSize = movies.PageSize,
                TotalCount = movies.TotalCount
               
            };
            
        }
       
        public async Task<MovieResponce?> GetAsync(string idOrSlug,string userId , CancellationToken token = default)
        {
            var movie = Guid.TryParse(idOrSlug, out var id)
               ? await _movieRepository.GetByIdAsync(id , token)
               : await _movieRepository.GetBySlugAsync(idOrSlug ,token);

            if (movie is null)
            {
                return null;
            }

            var (avg, userRating) = await _ratingRepository.GetAverageAndUserRatingAsync(
                                                                    movie.Id, userId, token);

        return new MovieResponce
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    YearOfRelease = movie.YearOfRelease,
                    Slug = movie.Slug,
                    Genres  = movie.Genres.Select(g => g.Name)
,
                    Rating = avg,
                    UserRating = userRating
                };
        }

        public async Task<MovieResponce?> UpdateAsync(UpdateMovieRequste updateMovie 
                                                      , Guid id
                                                      ,string userId
                                                      , CancellationToken token = default)
        {
            await _updateValidator.ValidateAndThrowAsync(updateMovie , token);

           var movie = await _movieRepository.GetByIdAsync(id , token);

            if (movie is null) return null;
            var (avg, userRating) = await _ratingRepository.GetAverageAndUserRatingAsync(
                                                        movie.Id, userId, token);


            movie.Title = updateMovie.Title;
            movie.YearOfRelease = updateMovie.YearOfRelease; 
            movie.Slug = SlugGenerator.Generate(updateMovie.Title, updateMovie.YearOfRelease);
            if (updateMovie.GenreIds?.Any() == true)
            {
                var genres = await _genreRepository.GetByIdsAsync(updateMovie.GenreIds, token);
                movie.Genres = genres.ToList();
            }

            await _movieRepository.SaveAsync(token);

            return new MovieResponce
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Slug = movie.Slug,
                Genres = movie.Genres.Select(g => g.Name),
                Rating = avg,
                UserRating = userRating
            };
        }
    }
}