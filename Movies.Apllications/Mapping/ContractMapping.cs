using Movies.Applications.DataBaces.Models;
using Movies.Applications.MovieRepositories;
using Movies.Applications.Utilities;
using Movies.Contracts.DTO;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;
using System.Runtime.CompilerServices;

namespace Movies.Applications.Mapping
{
    public static class ContractMapping
    {

//        public static Movie MapToMovie(this CreateMovieRequste request)
//        {
//            return new Movie
//            {
//                Id = Guid.NewGuid(),
//                Title = request.Title,
//                YearOfRelease = request.YearOfRelease,
//                Genres = request.GenreIds.ToList(),
//                Slug = SlugGenerator.Generate(request.Title , request.YearOfRelease)
//            };
//        }
//        public static Movie MapToMovie(this UpdateMovieRequste request , Guid id)
//        {
//            return new Movie
//            {
//                Id = id,
//                Title = request.Title,
//                YearOfRelease = request.YearOfRelease,
//                Genres = request.Genres.ToList()
//            };
//        }
//        public static MovieResponce MapToResponce(this Movie movie)
//        {
//            return new MovieResponce
//            {
//                Id = movie.Id,
//                Title = movie.Title,
//                Slug = movie.Slug,
//                YearOfRelease = movie.YearOfRelease,
//                Genres = movie.Genres 
//            };    

//        }

//        public static MoviesResponce MapToResponce(this IEnumerable<Movie> movies)
//        {
//            return new MoviesResponce
//            {
//                Items = movies.Select(MapToResponce)
//            };
//        }


    }
}
