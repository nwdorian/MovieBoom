namespace Application.Movies.Responses;

public record class GetMoviesResponse(
    Guid Id,
    string Title,
    string GenreName,
    DateOnly ReleaseDate,
    decimal Price,
    int Rating
);
