using Domain.Genres;
using Domain.Movies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Movies;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.UserId);

        builder.Property(m => m.Title).HasMaxLength(100);

        builder.Property(m => m.Price).HasPrecision(18, 2);

        builder.HasOne<Genre>().WithMany().HasForeignKey(m => m.GenreId);

        builder.HasOne<IdentityUser<Guid>>().WithMany().HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
