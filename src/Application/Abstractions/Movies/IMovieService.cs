using Application.Common;
using Application.Movies.Queries;
using Application.Movies.Responses;

namespace Application.Abstractions.Movies;

public interface IMovieService
{
    Task<PagedList<GetMoviesResponse>> GetMoviesPage(GetMoviesQuery query, CancellationToken cancellationToken);
}
