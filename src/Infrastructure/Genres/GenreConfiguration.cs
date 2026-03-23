using Domain.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Genres;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name).HasMaxLength(20);

        builder.HasIndex(g => g.Name).IsUnique();
    }
}
