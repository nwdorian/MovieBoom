namespace Application.Movies.Commands;

public record class UpdateMovieCommand(
    Guid Id,
    Guid GenreId,
    string Title,
    DateOnly ReleaseDate,
    decimal Price,
    int Rating
);
