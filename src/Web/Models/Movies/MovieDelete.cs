using System.ComponentModel.DataAnnotations;
using Application.Movies.Responses;

namespace Web.Models.Movies;

public class MovieDelete
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    [Display(Name = "Release date")]
    public required DateOnly ReleaseDate { get; set; }

    [DataType(DataType.Currency)]
    public required decimal Price { get; set; }
    public required int Rating { get; set; }

    public static MovieDelete Empty =>
        new()
        {
            Id = Guid.Empty,
            Title = string.Empty,
            Genre = string.Empty,
            ReleaseDate = DateOnly.FromDateTime(DateTime.MinValue),
            Price = 0,
            Rating = 0,
        };

    public static MovieDelete Create(GetMovieByIdResponse movie)
    {
        return new()
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.GenreName,
            ReleaseDate = movie.ReleaseDate,
            Price = movie.Price,
            Rating = movie.Rating,
        };
    }
}
