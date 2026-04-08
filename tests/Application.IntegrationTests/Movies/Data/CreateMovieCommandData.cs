using Application.Movies.Commands;
using Infrastructure.Genres;

namespace Application.IntegrationTests.Movies.Data;

public static class CreateMovieCommandData
{
    public static CreateMovieCommand Create()
    {
        return new(GenreFaker.ActionId, "Example", DateOnly.FromDateTime(DateTime.Now), 10, 10);
    }
}
