using Application.Abstractions.Movies;
using Application.IntegrationTests.Core;
using Application.Movies.Queries;
using Application.Movies.Responses;
using Domain.Common.Results;
using Domain.Genres;
using Domain.Movies;
using Infrastructure.Genres;
using Infrastructure.Movies;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.IntegrationTests.Movies;

public class GetMovieByIdTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IMovieService _movieService => Factory.Services.GetRequiredService<IMovieService>();

    [Fact]
    public async Task GetById_ShouldReturnSuccess_WhenMovieExists()
    {
        Movie expectedMovie = MovieFaker.Create().Single(m => m.Id == MovieFaker.ShawshankId);
        Genre expectedGenre = GenreFaker.Create().Single(g => g.Id == expectedMovie.GenreId);
        GetMovieByIdQuery query = new(expectedMovie.Id);

        Result<GetMovieByIdResponse> result = await _movieService.GetById(query, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(expectedMovie.Id);
        result.Value.GenreId.ShouldBe(expectedMovie.GenreId);
        result.Value.Title.ShouldBe(expectedMovie.Title);
        result.Value.GenreName.ShouldBe(expectedGenre.Name);
        result.Value.ReleaseDate.ShouldBe(expectedMovie.ReleaseDate);
        result.Value.Price.ShouldBe(expectedMovie.Price);
        result.Value.Rating.ShouldBe(expectedMovie.Rating);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenMovieDoesNotExist()
    {
        GetMovieByIdQuery query = new(Guid.Empty);

        Result<GetMovieByIdResponse> result = await _movieService.GetById(query, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBeEquivalentTo(MovieErrors.NotFoundById(Guid.Empty));
    }
}
