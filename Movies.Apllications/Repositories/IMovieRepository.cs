using Movies.Applications.DataBaces.Models;
using Movies.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.MovieRepositories
{
    public interface IMovieRepository
    {
        public Task SaveAsyn();
        Task CreateAsync(Movie movie);
        Task<Movie> GetByIdAsync (Guid id);
        Task<Movie> GetBySlugAsync (string Slug);
        Task<IEnumerable<Movie>> GetAllAsync();
    }
}
