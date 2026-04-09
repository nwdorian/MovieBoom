using Application.Abstractions.Movies;
using Application.IntegrationTests.Core;
using Application.Movies.Commands;
using Domain.Common.Results;
using Domain.Movies;
using Infrastructure.Movies;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.IntegrationTests.Movies;

public class DeleteMovieTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IMovieService _movieService => Scope.ServiceProvider.GetRequiredService<IMovieService>();

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenMovieDoesNotExist()
    {
        DeleteMovieCommand command = new(Guid.Empty);

        Result result = await _movieService.Delete(command, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBeEquivalentTo(MovieErrors.NotFoundById(Guid.Empty));
    }

    [Fact]
    public async Task Delete_ShouldReturnSuccess_WhenMovieWasDeleted()
    {
        DeleteMovieCommand command = new(MovieFaker.ShawshankId);

        Result result = await _movieService.Delete(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        DbContext.Movies.Any(m => m.Id == MovieFaker.ShawshankId).ShouldBeFalse();
    }
}
