using Application.Genres.Responses;

namespace Application.Abstractions.Genres;

public interface IGenreService
{
    Task<List<GetGenresResponse>> GetAllGenres(CancellationToken cancellationToken);
}
