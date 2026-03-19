namespace Domain.Genres;

public sealed class Genre
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
