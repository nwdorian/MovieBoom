using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Application.Genres.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Movies;

public class MovieCreate
{
    public SelectList? Genres { get; set; }

    [Required(ErrorMessage = "Please select a genre")]
    [JsonRequired]
    [Display(Name = "Genre")]
    public Guid GenreId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [JsonRequired]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    [Display(Name = "Release date")]
    public DateOnly ReleaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [JsonRequired]
    [Range(0.01, double.MaxValue, ErrorMessage = "Value must be higher than 0")]
    public decimal Price { get; set; }

    [JsonRequired]
    [Range(0, 10)]
    public int Rating { get; set; }

    public static MovieCreate Create(IReadOnlyList<GetGenresResponse> genres)
    {
        MovieCreate model = new();
        model.SetGenres(genres);
        return model;
    }

    public void SetGenres(IReadOnlyList<GetGenresResponse> genres)
    {
        Genres = new SelectList(genres, "Id", "Name");
    }
}
