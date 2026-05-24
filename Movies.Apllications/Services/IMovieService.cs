using Movies.Applications.Enums;
using Movies.Applications.DataBaces.Models;
using Movies.Contracts.DTO;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;

namespace Movies.Applications.Services
{
    public interface IMovieService
    {
       Task<MovieResponce> CreateAsync(CreateMovieRequste movieRequest, CancellationToken token = default);
        Task<MoviesResponce> GetAllAsync( CancellationToken token = default);
        Task<MovieResponce?> GetAsync(string idOrSlug, CancellationToken token = default);
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken token = default);
        Task<MovieResponce?> UpdateAsync(UpdateMovieRequste movieUpdate, Guid id, CancellationToken token = default);

    }
}
