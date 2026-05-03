using Movies.Applications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.MovieRpositories
{
    public interface IMovieRpository
    {
        Task<bool> CreatAsync(Movie movie);
        Task<Movie> GetByIdAsync (Guid id);
        Task<Movie> GetBySlugAsync (string Slug);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<bool> UpdateAsync(Movie movie);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
