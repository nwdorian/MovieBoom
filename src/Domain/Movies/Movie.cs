namespace Domain.Movies;

public sealed class Movie
{
    public Guid Id { get; set; }
    public Guid GenreId { get; set; }
    public required string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public int Rating { get; set; }
}
