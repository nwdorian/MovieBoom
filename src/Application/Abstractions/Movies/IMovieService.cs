using Application.Common;
using Application.Movies.Commands;
using Application.Movies.Queries;
using Application.Movies.Responses;
using Domain.Common.Results;

namespace Application.Abstractions.Movies;

public interface IMovieService
{
    Task<PagedList<GetMoviesResponse>> GetMoviesPage(GetMoviesQuery query, CancellationToken cancellationToken);
    Task<Result> Create(CreateMovieCommand command, CancellationToken cancellationToken);
    Task<Result> Delete(DeleteMovieCommand command, CancellationToken cancellationToken);
}
