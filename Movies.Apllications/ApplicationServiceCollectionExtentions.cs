using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.MovieRepositories;
using Movies.Applications.Repositories;
using Movies.Applications.Services;
using FluentValidation;
using Movies.Applications.Validator;

namespace Movies.Applications
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services ,
                 IConfiguration configuration)
        {
            services.AddDbContext<RestDBContext>(option =>
                                 option.UseSqlServer(
                                    configuration.GetConnectionString("RestConnection")));

            services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
            services.AddScoped<IMovieRepository,MovieRepository>();
            services.AddScoped<IMovieService,MovieService>();
            services.AddScoped<IGenreRepository, GenreRpository>();
            services.AddScoped<IGenreService, GenreService>();

            return services;
        }
    }
}
