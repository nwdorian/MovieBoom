using Application.Genres.Responses;

namespace Application.Abstractions.Genres;

public interface IGenreService
{
    Task<IReadOnlyList<GetGenresResponse>> GetAllGenres(CancellationToken cancellationToken);
}
