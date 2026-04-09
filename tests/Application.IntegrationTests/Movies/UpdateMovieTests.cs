using Application.Abstractions.Movies;
using Application.IntegrationTests.Core;
using Application.Movies.Commands;
using Domain.Common.Results;
using Domain.Genres;
using Domain.Movies;
using Infrastructure.Genres;
using Infrastructure.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.IntegrationTests.Movies;

public class UpdateMovieTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IMovieService _movieService => Scope.ServiceProvider.GetRequiredService<IMovieService>();

    [Fact]
    public async Task Update_ShouldReturnMovieNotFound_WhenMovieDoesNotExist()
    {
        UpdateMovieCommand command = new(
            Guid.Empty,
            GenreFaker.ActionId,
            "Updated Title",
            DateOnly.FromDateTime(DateTime.Now),
            19.99m,
            5
        );

        Result result = await _movieService.Update(command, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBeEquivalentTo(MovieErrors.NotFoundById(Guid.Empty));
    }

    [Fact]
    public async Task Update_ShouldReturnGenreNotFound_WhenGenreDoesNotExist()
    {
        UpdateMovieCommand command = new(
            MovieFaker.ShawshankId,
            Guid.Empty,
            "Updated Title",
            DateOnly.FromDateTime(DateTime.Now),
            19.99m,
            5
        );

        Result result = await _movieService.Update(command, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBeEquivalentTo(GenreErrors.NotFoundById(Guid.Empty));
    }

    [Fact]
    public async Task Update_ShouldReturnSuccess_WhenMovieWasUpdated()
    {
        UpdateMovieCommand command = new(
            MovieFaker.ShawshankId,
            GenreFaker.ComedyId,
            "Updated Title",
            new DateOnly(2024, 1, 1),
            19.99m,
            8
        );

        Result result = await _movieService.Update(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();

        Movie? updatedMovie = await DbContext.Movies.FirstOrDefaultAsync(
            m => m.Id == MovieFaker.ShawshankId,
            CancellationToken.None
        );

        updatedMovie.ShouldNotBeNull();
        updatedMovie.Title.ShouldBe(command.Title);
        updatedMovie.GenreId.ShouldBe(command.GenreId);
        updatedMovie.ReleaseDate.ShouldBe(command.ReleaseDate);
        updatedMovie.Price.ShouldBe(command.Price);
        updatedMovie.Rating.ShouldBe(command.Rating);
    }
}
