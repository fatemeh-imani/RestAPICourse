using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Applications.DataBaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.DataBaces.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                 .IsRequired()
                 .HasMaxLength(200);

            builder.Property(m => m.YearOfRelease)
                .IsRequired();

            builder.HasMany(m => m.Genres)
                .WithMany(m => m.Movies)
                .UsingEntity(j => j.ToTable("MovieGeners"));

            builder.Property(m => m.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(m => m.Slug)
                .IsUnique();
        }
    }
    
}
