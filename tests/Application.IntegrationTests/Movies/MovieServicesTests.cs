using Application.Abstractions.Movies;
using Application.IntegrationTests.Core;
using Application.IntegrationTests.Movies.Data;
using Application.Movies.Commands;
using Domain.Common.Results;
using Domain.Genres;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.IntegrationTests.Movies;

public class MovieServicesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IMovieService _movieService => Factory.Services.GetRequiredService<IMovieService>();

    [Fact]
    public async Task Create_ShouldReturnGenreNotFound_WhenGenreDoesNotExist()
    {
        CreateMovieCommand command = CreateMovieCommandData.Create() with { GenreId = Guid.Empty };

        Result result = await _movieService.Create(command, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBeEquivalentTo(GenreErrors.NotFoundById(Guid.Empty));
    }

    [Fact]
    public async Task Create_ShouldReturnSuccess_WhenMovieWasCreated()
    {
        CreateMovieCommand command = CreateMovieCommandData.Create();

        Result result = await _movieService.Create(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
    }
}
