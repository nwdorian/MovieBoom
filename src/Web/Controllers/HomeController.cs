using System.Diagnostics;
using Application.Abstractions.Genres;
using Application.Abstractions.Movies;
using Application.Common;
using Application.Genres.Responses;
using Application.Movies.Commands;
using Application.Movies.Pagination;
using Application.Movies.Queries;
using Application.Movies.Responses;
using Domain.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
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
    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return PartialView(
            Partials.CreateMovie,
            MovieCreate.Create(await genreService.GetAllGenres(cancellationToken))
        );
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(MovieCreate model, CancellationToken cancellationToken)
    {
        IReadOnlyList<GetGenresResponse> genres = await genreService.GetAllGenres(cancellationToken);

        if (!ModelState.IsValid)
        {
            model.SetGenres(genres);
            return PartialView(Partials.CreateMovie, model);
        }

        CreateMovieCommand command = new(model.GenreId, model.Title, model.ReleaseDate, model.Price, model.Rating);
        Result create = await movieService.Create(command, cancellationToken);
        if (create.IsFailure)
        {
            ModelState.AddModelError(string.Empty, create.Error.Description);
            model.SetGenres(genres);
            return PartialView(Partials.CreateMovie, model);
        }

        return Created();
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
