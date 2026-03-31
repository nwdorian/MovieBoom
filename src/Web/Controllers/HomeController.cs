using System.Diagnostics;
using Application.Abstractions.Genres;
using Application.Abstractions.Movies;
using Application.Common;
using Application.Genres.Responses;
using Application.Movies.Pagination;
using Application.Movies.Queries;
using Application.Movies.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Movies;
using Web.Models.Movies.Requests;

namespace Web.Controllers;

public class HomeController(IMovieService movieService, IGenreService genreService) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index(GetMoviesRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        MovieFilter filter = new(request.SearchTerm, request.GenreName, request.StartPrice, request.EndPrice);
        MovieSorting sorting = new(request.SortColumn, request.SortOrder);
        Paging paging = new(request.PageNumber, request.PageSize);
        GetMoviesQuery query = new(filter, sorting, paging);

        PagedList<GetMoviesResponse> page = await movieService.GetMoviesPage(query, cancellationToken);
        IReadOnlyList<GetGenresResponse> genres = await genreService.GetAllGenres(cancellationToken);

        return View(MovieIndex.Create(request, page, genres));
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
