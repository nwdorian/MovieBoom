using Application.Abstractions.Database;
using Application.Abstractions.Genres;
using Application.Genres.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Genres;

public class GenreService(IApplicationDbContext dbContext) : IGenreService
{
    public async Task<IReadOnlyList<GetGenresResponse>> GetAllGenres(CancellationToken cancellationToken)
    {
        return await dbContext.Genres.Select(g => new GetGenresResponse(g.Id, g.Name)).ToListAsync(cancellationToken);
    }
}
