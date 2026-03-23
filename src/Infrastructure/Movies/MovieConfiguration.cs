using Domain.Movies;
using Infrastructure.Users;
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

        builder.HasOne(m => m.Genre).WithMany(g => g.Movies).HasForeignKey(m => m.GenreId);

        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
