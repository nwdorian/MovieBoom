using Domain.Movies;
using Infrastructure.Genres;
using Infrastructure.Users;

namespace Infrastructure.Movies;

public static class MovieFaker
{
    public static readonly Guid ShawshankId = Guid.Parse("1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d");
    public static readonly Guid ParasiteId = Guid.Parse("2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e");
    public static readonly Guid WhiplashId = Guid.Parse("3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f");
    public static readonly Guid ForrestGumpId = Guid.Parse("4d5e6f7a-8b9c-4d0e-1f2a-3b4c5d6e7f8a");
    public static readonly Guid InceptionId = Guid.Parse("5e6f7a8b-9c0d-4e1f-2a3b-4c5d6e7f8a9b");
    public static readonly Guid MatrixId = Guid.Parse("6f7a8b9c-0d1e-4f2a-3b4c-5d6e7f8a9b0c");
    public static readonly Guid InterstellarId = Guid.Parse("7a8b9c0d-1e2f-4a3b-4c5d-6e7f8a9b0c1d");
    public static readonly Guid ArrivalId = Guid.Parse("8b9c0d1e-2f3a-4b4c-5d6e-7f8a9b0c1d2e");
    public static readonly Guid ToyStoryId = Guid.Parse("9c0d1e2f-3a4b-4c5d-6e7f-8a9b0c1d2e3f");
    public static readonly Guid SpiderVerseId = Guid.Parse("0d1e2f3a-4b5c-4d6e-7f8a-9b0c1d2e3f4a");
    public static readonly Guid CocoId = Guid.Parse("1e2f3a4b-5c6d-4e7f-8a9b-0c1d2e3f4a5b");
    public static readonly Guid JokerId = Guid.Parse("2f3a4b5c-6d7e-4f8a-9b0c-1d2e3f4a5b6c");
    public static readonly Guid PulpFictionId = Guid.Parse("3a4b5c6d-7e8f-4a9b-0c1d-2e3f4a5b6c7d");
    public static readonly Guid FightClubId = Guid.Parse("4b5c6d7e-8f9a-4b0c-1d2e-3f4a5b6c7d8e");
    public static readonly Guid DarkKnightId = Guid.Parse("5c6d7e8f-9a0b-4c1d-2e3f-4a5b6c7d8e9f");
    public static readonly Guid HangoverId = Guid.Parse("6d7e8f9a-0b1c-4d2e-3f4a-5b6c7d8e9f0a");
    public static readonly Guid GetOutId = Guid.Parse("7e8f9a0b-1c2d-4e3f-4a5b-6c7d8e9f0a1b");
    public static readonly Guid PlanetEarthId = Guid.Parse("8f9a0b1c-2d3e-4f4a-5b6c-7d8e9f0a1b2c");
    public static readonly Guid TitanicId = Guid.Parse("9a0b1c2d-3e4f-4a5b-6c7d-8e9f0a1b2c3d");
    public static readonly Guid LotrId = Guid.Parse("0b1c2d3e-4f5a-4b6c-7d8e-9f0a1b2c3d4e");

    public static List<Movie> Create() =>
        new()
        {
            new()
            {
                Id = ShawshankId,
                Title = "The Shawshank Redemption",
                GenreId = GenreFaker.DramaId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(1994, 9, 23),
                Price = 9.99m,
                Rating = 9,
            },
            new()
            {
                Id = ParasiteId,
                Title = "Parasite",
                GenreId = GenreFaker.DramaId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2019, 5, 30),
                Price = 11.99m,
                Rating = 8,
            },
            new()
            {
                Id = WhiplashId,
                Title = "Whiplash",
                GenreId = GenreFaker.DramaId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2014, 10, 10),
                Price = 10.99m,
                Rating = 9,
            },
            new()
            {
                Id = ForrestGumpId,
                Title = "Forrest Gump",
                GenreId = GenreFaker.DramaId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(1994, 7, 6),
                Price = 9.99m,
                Rating = 9,
            },
            new()
            {
                Id = InceptionId,
                Title = "Inception",
                GenreId = GenreFaker.SciFiId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2010, 7, 16),
                Price = 12.99m,
                Rating = 9,
            },
            new()
            {
                Id = MatrixId,
                Title = "The Matrix",
                GenreId = GenreFaker.SciFiId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(1999, 3, 31),
                Price = 9.99m,
                Rating = 9,
            },
            new()
            {
                Id = InterstellarId,
                Title = "Interstellar",
                GenreId = GenreFaker.SciFiId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2014, 11, 7),
                Price = 12.99m,
                Rating = 9,
            },
            new()
            {
                Id = ArrivalId,
                Title = "Arrival",
                GenreId = GenreFaker.SciFiId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2016, 11, 11),
                Price = 10.99m,
                Rating = 8,
            },
            new()
            {
                Id = ToyStoryId,
                Title = "Toy Story",
                GenreId = GenreFaker.AnimationId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(1995, 11, 22),
                Price = 7.99m,
                Rating = 8,
            },
            new()
            {
                Id = SpiderVerseId,
                Title = "Spider-Man: Into the Spider-Verse",
                GenreId = GenreFaker.AnimationId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2018, 12, 14),
                Price = 9.99m,
                Rating = 8,
            },
            new()
            {
                Id = CocoId,
                Title = "Coco",
                GenreId = GenreFaker.AnimationId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2017, 11, 22),
                Price = 9.99m,
                Rating = 8,
            },
            new()
            {
                Id = JokerId,
                Title = "Joker",
                GenreId = GenreFaker.ThrillerId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2019, 10, 4),
                Price = 10.99m,
                Rating = 8,
            },
            new()
            {
                Id = PulpFictionId,
                Title = "Pulp Fiction",
                GenreId = GenreFaker.ThrillerId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(1994, 10, 14),
                Price = 9.99m,
                Rating = 9,
            },
            new()
            {
                Id = FightClubId,
                Title = "Fight Club",
                GenreId = GenreFaker.ThrillerId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(1999, 10, 15),
                Price = 9.99m,
                Rating = 9,
            },
            new()
            {
                Id = DarkKnightId,
                Title = "The Dark Knight",
                GenreId = GenreFaker.ActionId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2008, 7, 18),
                Price = 14.99m,
                Rating = 9,
            },
            new()
            {
                Id = HangoverId,
                Title = "The Hangover",
                GenreId = GenreFaker.ComedyId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2009, 6, 5),
                Price = 8.99m,
                Rating = 8,
            },
            new()
            {
                Id = GetOutId,
                Title = "Get Out",
                GenreId = GenreFaker.HorrorId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2017, 2, 24),
                Price = 10.99m,
                Rating = 8,
            },
            new()
            {
                Id = PlanetEarthId,
                Title = "Planet Earth II",
                GenreId = GenreFaker.DocumentaryId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2016, 2, 18),
                Price = 6.99m,
                Rating = 9,
            },
            new()
            {
                Id = TitanicId,
                Title = "Titanic",
                GenreId = GenreFaker.RomanceId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(1997, 12, 19),
                Price = 8.99m,
                Rating = 8,
            },
            new()
            {
                Id = LotrId,
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                GenreId = GenreFaker.FantasyId,
                UserId = UserFaker.UserId,
                ReleaseDate = new DateOnly(2001, 12, 19),
                Price = 12.99m,
                Rating = 9,
            },
        };
}
