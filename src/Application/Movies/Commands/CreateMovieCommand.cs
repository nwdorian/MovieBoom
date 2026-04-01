namespace Application.Movies.Commands;

public record class CreateMovieCommand(Guid GenreId, string Title, DateOnly ReleaseDate, decimal Price, int Rating);
