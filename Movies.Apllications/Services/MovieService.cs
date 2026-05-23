using Azure.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.Enums;
using Movies.Applications.MovieRepositories;
using Movies.Applications.Utilities;
using Movies.Contracts.DTO;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Movies.Applications.Services
{
    public class MovieService(
        IMovieRepository _movieRepository,
        IGenreRepository _genreRepository,
        IValidator<CreateMovieRequste> _createValidator,
         IValidator<UpdateMovieRequste> _updateValidator
        ) : IMovieService
    {
        public async Task<MovieResponce> CreateAsync(CreateMovieRequste movieRequest)
        {
            await _createValidator.ValidateAndThrowAsync( movieRequest );

            var slug = SlugGenerator.Generate(movieRequest.Title, movieRequest.YearOfRelease);

            var existingMovie = await _movieRepository.GetBySlugAsync(slug);

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
                var genres = await _genreRepository.GetByIdsAsync(movieRequest.GenreIds);
                movie.Genres = genres.ToList();
            }

            await _movieRepository.CreateAsync(movie);

              await  _movieRepository.SaveAsyn();


            return new MovieResponce
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Slug = movie.Slug,
                Genres = movie.Genres.Select(g => g.Name)
            };
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
           var movie = await _movieRepository.GetByIdAsync(id);

            if (movie is null) return  false;

            movie.IsDeleted = true;

              await _movieRepository.SaveAsyn();
            return true;     
        }
        public async Task<MoviesResponce> GetAllAsync()
        {
          var movies = await _movieRepository.GetAllAsync();
            return new MoviesResponce
            {
                Items = movies.Select(movie => new MovieResponce
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    YearOfRelease = movie.YearOfRelease,
                    Slug = movie.Slug,
                    Genres = movie.Genres.Select(g => g.Name)
                })

            };
            
        }
        public async Task<MovieResponce?> GetAsync(string idOrSlug)
        {
            var movie = Guid.TryParse(idOrSlug, out var id)
               ? await _movieRepository.GetByIdAsync(id)
               : await _movieRepository.GetBySlugAsync(idOrSlug);

            if (movie is null)
            {
                return null;
            }

            return new MovieResponce
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Slug = movie.Slug,
                Genres = movie.Genres.Select(g => g.Name)
            };
        }

        public async Task<MovieResponce?> UpdateAsync(UpdateMovieRequste updateMovie , Guid id)
        {
            await _updateValidator.ValidateAndThrowAsync(updateMovie);

           var movie = await _movieRepository.GetByIdAsync(id);

            if (movie is null) return null;
            
            movie.Title = updateMovie.Title;
            movie.YearOfRelease = updateMovie.YearOfRelease; 
            movie.Slug = SlugGenerator.Generate(updateMovie.Title, updateMovie.YearOfRelease);
            if (updateMovie.GenreIds?.Any() == true)
            {
                var genres = await _genreRepository.GetByIdsAsync(updateMovie.GenreIds);
                movie.Genres = genres.ToList();
            }
            await _movieRepository.SaveAsyn();

            return new MovieResponce
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Slug = movie.Slug,
                Genres = movie.Genres.Select(g => g.Name)
            };
        }
    }
}