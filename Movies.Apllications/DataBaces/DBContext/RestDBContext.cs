using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Applications.DataBaces.Models;


namespace Movies.Applications.DataBaces.DBContext
{
    public class RestDBContext(DbContextOptions<RestDBContext> option) 
        :IdentityDbContext<ApplicationUser>(option)
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Genre>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Rating>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
