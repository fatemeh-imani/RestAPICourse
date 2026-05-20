using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Models;
using Movies.Contracts.SeedModel;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Movies.Applications.Utilities
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(RestDBContext _context)
        {
            if (await _context.Movies.AnyAsync())
            {
                return;
            }

            var json = await File.ReadAllTextAsync("Data/movies.json");

            var movies = JsonSerializer.Deserialize<List<MovieSeedModel>>(json);

            if (movies is null)
            {
                return;
            }

            foreach (var movieData in movies)
            {
                var genres = new List<Genre>();

                foreach (var genreName in movieData.Genres)
                {
                    var existingGenre = await _context.Genres
                        .FirstOrDefaultAsync(g => g.Name == genreName);

                    if (existingGenre is null)
                    {
                        existingGenre = new Genre
                        {
                            Id = Guid.NewGuid(),
                            Name = genreName
                        };

                        _context.Genres.Add(existingGenre);
                    }

                    genres.Add(existingGenre);
                }

                var movie = new Movie
                {
                    Id = movieData.Id,
                    Title = movieData.Title,
                    Slug = movieData.Slug,
                    YearOfRelease = movieData.YearOfRelease,
                    Genres = genres
                };

                _context.Movies.Add(movie);
            }

            await _context.SaveChangesAsync();
        }
    }
}
