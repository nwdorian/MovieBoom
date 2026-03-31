using Application.Abstractions.Genres;
using Application.Abstractions.Movies;
using Application.Genres;
using Application.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IGenreService, GenreService>();
    }
}
