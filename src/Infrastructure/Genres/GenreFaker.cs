using Domain.Genres;

namespace Infrastructure.Genres;

public static class GenreFaker
{
    public static readonly Guid ActionId = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d");
    public static readonly Guid ComedyId = Guid.Parse("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e");
    public static readonly Guid DramaId = Guid.Parse("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f");
    public static readonly Guid HorrorId = Guid.Parse("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a");
    public static readonly Guid SciFiId = Guid.Parse("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b");
    public static readonly Guid AnimationId = Guid.Parse("f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c");
    public static readonly Guid DocumentaryId = Guid.Parse("a7b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d");
    public static readonly Guid ThrillerId = Guid.Parse("b8c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e");
    public static readonly Guid RomanceId = Guid.Parse("c9d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f");
    public static readonly Guid FantasyId = Guid.Parse("d0e1f2a3-b4c5-4d6e-7f8a-9b0c1d2e3f4a");

    public static List<Genre> Create() =>
        new()
        {
            new() { Id = ActionId, Name = "Action" },
            new() { Id = ComedyId, Name = "Comedy" },
            new() { Id = DramaId, Name = "Drama" },
            new() { Id = HorrorId, Name = "Horror" },
            new() { Id = SciFiId, Name = "Sci-Fi" },
            new() { Id = AnimationId, Name = "Animation" },
            new() { Id = DocumentaryId, Name = "Documentary" },
            new() { Id = ThrillerId, Name = "Thriller" },
            new() { Id = RomanceId, Name = "Romance" },
            new() { Id = FantasyId, Name = "Fantasy" },
        };
}
