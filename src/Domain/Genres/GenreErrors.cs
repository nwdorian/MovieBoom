using Domain.Common.Results;

namespace Domain.Genres;

public static class GenreErrors
{
    public static Error NotFoundById(Guid id) =>
        Error.NotFound("Genre.NotFoundById", $"The genre with Id = {id} was not found.");
}
