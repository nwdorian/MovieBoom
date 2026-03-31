using System.ComponentModel.DataAnnotations;
using Application.Movies.Responses;

namespace Web.Models.Movies.Items;

public class MovieIndexItem(GetMoviesResponse movie)
{
    public Guid Id { get; set; } = movie.Id;
    public string Title { get; set; } = movie.Title;
    public string Genre { get; set; } = movie.GenreName;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    [Display(Name = "Release date")]
    public DateOnly ReleaseDate { get; set; } = movie.ReleaseDate;

    [DataType(DataType.Currency)]
    public decimal Price { get; set; } = movie.Price;
    public int Rating { get; set; } = movie.Rating;
}
