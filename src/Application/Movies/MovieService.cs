using Application.Abstractions.Database;
using Application.Abstractions.Movies;
using Application.Abstractions.Users;
using Application.Common;
using Application.Movies.Commands;
using Application.Movies.Pagination;
using Application.Movies.Queries;
using Application.Movies.Responses;
using Domain.Common.Results;
using Domain.Genres;
using Domain.Movies;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies;

public class MovieService(IApplicationDbContext dbContext, IUserContext userContext) : IMovieService
{
    public async Task<PagedList<GetMoviesResponse>> GetMoviesPage(
        GetMoviesQuery query,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Movie> moviesQuery = dbContext.Movies;

        moviesQuery = moviesQuery.Where(m => m.UserId == userContext.UserId);
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

    public async Task<Result<GetMovieByIdResponse>> GetById(
        GetMovieByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        GetMovieByIdResponse? response = await dbContext
            .Movies.Where(m => m.Id == query.Id && m.UserId == userContext.UserId)
            .Select(m => new GetMovieByIdResponse(m.Id, m.Title, m.Genre.Name, m.ReleaseDate, m.Price, m.Rating))
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
        {
            return MovieErrors.NotFoundById(query.Id);
        }

        return response;
    }

    public async Task<Result> Create(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        bool genreExists = await dbContext.Genres.AnyAsync(g => g.Id == command.GenreId, cancellationToken);
        if (!genreExists)
        {
            return GenreErrors.NotFoundById(command.GenreId);
        }

        Movie movie = new()
        {
            Id = Guid.NewGuid(),
            GenreId = command.GenreId,
            UserId = userContext.UserId,
            Title = command.Title,
            ReleaseDate = command.ReleaseDate,
            Price = command.Price,
            Rating = command.Rating,
        };

        dbContext.Movies.Add(movie);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Delete(DeleteMovieCommand command, CancellationToken cancellationToken)
    {
        Movie? movie = await dbContext.Movies.FirstOrDefaultAsync(
            m => m.Id == command.Id && m.UserId == userContext.UserId,
            cancellationToken
        );
        if (movie is null)
        {
            return MovieErrors.NotFoundById(command.Id);
        }

        dbContext.Movies.Remove(movie);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Update(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        Movie? movie = await dbContext
            .Movies.AsTracking()
            .FirstOrDefaultAsync(m => m.Id == command.Id && m.UserId == userContext.UserId, cancellationToken);
        if (movie is null)
        {
            return MovieErrors.NotFoundById(command.Id);
        }

        bool genreExists = await dbContext.Genres.AnyAsync(g => g.Id == command.GenreId, cancellationToken);
        if (!genreExists)
        {
            return GenreErrors.NotFoundById(command.GenreId);
        }

        movie.GenreId = command.GenreId;
        movie.Title = command.Title;
        movie.ReleaseDate = command.ReleaseDate;
        movie.Price = command.Price;
        movie.Rating = command.Rating;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
