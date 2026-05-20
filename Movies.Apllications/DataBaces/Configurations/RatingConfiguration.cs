using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Applications.DataBaces.Models;

namespace Movies.Applications.DataBaces.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(x => x.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(x => x.MovieId);

            builder.HasIndex(x => new { x.MovieId, x.UserId })
                .IsUnique();
            builder.Property(x => x.Score)
                   .IsRequired();

            builder.ToTable(t =>
                  t.HasCheckConstraint("CK_Rating_Score", "[Score] >= 1 AND [Score] <= 5"));
            //این باعث می‌شود دیتابیس اجازه ندهد امتیاز خارج از بازه ثبت شود.

        }
    }
}
