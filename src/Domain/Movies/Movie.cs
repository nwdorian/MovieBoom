using Domain.Genres;

namespace Domain.Movies;

public sealed class Movie
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GenreId { get; set; }
    public required string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public Genre? Genre { get; set; }
}
