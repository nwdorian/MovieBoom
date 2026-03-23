using Domain.Genres;
using Domain.Movies;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Database;

public interface IApplicationDbContext
{
    DbSet<Movie> Movies { get; }
    DbSet<Genre> Genres { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
