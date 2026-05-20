using Movies.Applications.Enums;
using Movies.Applications.DataBaces.Models;
using Movies.Contracts.DTO;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;

namespace Movies.Applications.Services
{
    public interface IMovieService
    {
       Task<MovieResponce> CreateAsync(CreateMovieRequste movieRequest);
        Task<MoviesResponce> GetAllAsync();
        Task<MovieResponce?> GetAsync(string idOrSlug);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<MovieResponce?> UpdateAsync(UpdateMovieRequste movieUpdate, Guid id);

    }
}
