using Application.Abstractions.Movies;
using Application.IntegrationTests.Core;
using Application.Movies.Commands;
using Domain.Common.Results;
using Domain.Genres;
using Infrastructure.Genres;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.IntegrationTests.Movies;

public class CreateMovieTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IMovieService _movieService => Scope.ServiceProvider.GetRequiredService<IMovieService>();

    [Fact]
    public async Task Create_ShouldReturnGenreNotFound_WhenGenreDoesNotExist()
    {
        CreateMovieCommand command = new(Guid.Empty, "Test", DateOnly.FromDateTime(DateTime.Now), 10, 10);

        Result result = await _movieService.Create(command, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBeEquivalentTo(GenreErrors.NotFoundById(Guid.Empty));
    }

    [Fact]
    public async Task Create_ShouldReturnSuccess_WhenMovieWasCreated()
    {
        CreateMovieCommand command = new(GenreFaker.ActionId, "Test", DateOnly.FromDateTime(DateTime.Now), 10, 10);

        Result result = await _movieService.Create(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
    }
}
