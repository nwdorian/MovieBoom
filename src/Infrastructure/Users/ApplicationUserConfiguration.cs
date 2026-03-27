using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(120);
        builder.Property(u => u.LastName).HasMaxLength(120);
    }
}
