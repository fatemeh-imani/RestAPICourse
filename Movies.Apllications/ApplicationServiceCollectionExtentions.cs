using Microsoft.Extensions.DependencyInjection;
using Movies.Applications.MovieRpositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications
{
    public static class ApplicationServiceCollectionExtentions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddSingleton<IMovieRpository,MovieRepository>();
            return services;
        }
    }
}
