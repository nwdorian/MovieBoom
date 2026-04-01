using Domain.Common.Results;

namespace Domain.Movies;

public static class MovieErrors
{
    public static Error NotFoundById(Guid id) =>
        Error.NotFound("Movie.NotFoundById", $"The movie with Id = {id} was not found.");
}
