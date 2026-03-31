namespace Application.Movies.Pagination;

public record class MovieFilter(string? SearchTerm, string? GenreName, decimal? StartPrice, decimal? EndPrice);
