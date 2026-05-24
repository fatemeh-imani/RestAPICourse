using Movies.Applications.DataBaces.Models;


namespace Movies.Applications.MovieRepositories
{
    public interface IMovieRepository
    {
        public Task SaveAsync(CancellationToken token = default);
        Task CreateAsync(Movie movie, CancellationToken token = default);
        Task<Movie> GetByIdAsync (Guid id, CancellationToken token = default);
        Task<Movie> GetBySlugAsync (string Slug, CancellationToken token = default);
        Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default);
    }
}
