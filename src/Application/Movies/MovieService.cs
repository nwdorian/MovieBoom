using Application.Abstractions.Database;
using Application.Abstractions.Movies;
using Application.Common;
using Application.Movies.Pagination;
using Application.Movies.Queries;
using Application.Movies.Responses;
using Domain.Movies;

namespace Application.Movies;

public class MovieService(IApplicationDbContext dbContext) : IMovieService
{
    public async Task<PagedList<GetMoviesResponse>> GetMoviesPage(
        GetMoviesQuery query,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Movie> moviesQuery = dbContext.Movies;

        moviesQuery = moviesQuery.ApplyFiltering(query.Filter);
        moviesQuery = moviesQuery.ApplySorting(query.Sorting);

        IQueryable<GetMoviesResponse> movieResponsesQuery = moviesQuery.Select(m => new GetMoviesResponse(
            m.Id,
            m.Title,
            m.Genre.Name,
            m.ReleaseDate,
            m.Price,
            m.Rating
        ));

        return await PagedList<GetMoviesResponse>.Create(movieResponsesQuery, query.Paging, cancellationToken);
    }
}
