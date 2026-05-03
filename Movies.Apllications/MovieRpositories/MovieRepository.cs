using Movies.Applications.Models;
using Movies.Applications.MovieRpositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.MovieRpositories
{
    public class MovieRepository : IMovieRpository
    {
        public List<Movie> _movie = new();
        public Task<bool> CreatAsync(Movie movie)
        {
            _movie.Add(movie);
            return Task.FromResult(true);
        }
        public Task<Movie> GetByIdAsync(Guid id)
        {
            var movie = _movie.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(movie);
        }

        public Task<Movie> GetBySlugAsync(string slug)
        {
            var movie = _movie.SingleOrDefault(x => x.Slug == slug);
            return Task.FromResult(movie);
        }
        public Task<bool> DeleteByIdAsync(Guid id)
        {
            var removedCount = _movie.RemoveAll(x => x.Id == id);
            var movieRemoved = removedCount > 0;

            return Task.FromResult(movieRemoved);
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            return Task.FromResult(_movie.AsEnumerable());
        }

       

        public Task<bool> UpdateAsync(Movie movie)
        {
            var movieIndex = _movie.FindIndex(x => x.Id == movie.Id);
            if (movieIndex == -1)
            {
                return Task.FromResult(false);
            }

            _movie[movieIndex] = movie;
            return Task.FromResult(true);
        }

       
    }
}
