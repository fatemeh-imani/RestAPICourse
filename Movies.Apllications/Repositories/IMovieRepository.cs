using Movies.Applications.DataBaces.Models;
using Movies.Applications.Options;
using Movies.Applications.Projections;


namespace Movies.Applications.MovieRepositories
{
    public interface IMovieRepository
    {
        public Task SaveAsync(CancellationToken token = default);
        Task CreateAsync(Movie movie, CancellationToken token = default);
        Task<Movie?> GetByIdAsync (Guid id, CancellationToken token = default);
        Task<Movie> GetBySlugAsync (string Slug, CancellationToken token = default);
        Task<IEnumerable<MovieReadModel>> GetAllAsync(GetAllMovieOption option, CancellationToken token = default);
    }
}
