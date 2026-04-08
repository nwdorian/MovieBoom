using Application.Abstractions.Movies;
using Application.IntegrationTests.Core;
using Application.Movies.Commands;
using Domain.Common.Results;
using Infrastructure.Genres;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.IntegrationTests.Movies;

public class MovieServicesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IMovieService _movieService => Factory.Services.GetRequiredService<IMovieService>();

    [Fact]
    public async Task Create_ShouldReturnGenreNotFound_WhenGenreDoesNotExist()
    {
        CreateMovieCommand command = new(GenreFaker.ActionId, "Example", DateOnly.FromDateTime(DateTime.Now), 10, 10);

        Result result = await _movieService.Create(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
    }
}
