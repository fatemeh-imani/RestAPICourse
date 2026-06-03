
using Movies.Applications.DataBaces.Models;
using Movies.Applications.Pagination;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;

namespace Movies.Applications.Services
{
    public interface IMovieService
    {
       Task<MovieResponce> CreateAsync(CreateMovieRequste movieRequest, CancellationToken token = default);
        Task<PagedResponse<MovieResponce>> GetAllAsync(GetAllMovieRequest requect, string userId , CancellationToken token = default);
        Task<MovieResponce?> GetAsync(string idOrSlug , string userId, CancellationToken token = default);
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken token = default);
        Task<MovieResponce?> UpdateAsync(UpdateMovieRequste movieUpdate, Guid id,string userId, CancellationToken token = default);

    }
}
