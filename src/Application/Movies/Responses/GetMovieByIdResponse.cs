namespace Application.Movies.Responses;

public record class GetMovieByIdResponse(
    Guid Id,
    Guid GenreId,
    string Title,
    string GenreName,
    DateOnly ReleaseDate,
    decimal Price,
    int Rating
);
