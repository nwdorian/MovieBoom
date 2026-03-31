using Application.Common;
using Application.Genres.Responses;
using Application.Movies.Pagination;
using Application.Movies.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Constants;
using Web.Models.Movies.Items;
using Web.Models.Movies.Requests;
using Web.Models.Shared;

namespace Web.Models.Movies;

public class MovieIndex
{
    public IReadOnlyList<MovieIndexItem> Movies { get; set; } = [];
    public PagingMetadata PagingMetadata { get; set; } = null!;
    public SelectList Genres { get; set; } = null!;
    public SelectList SortColumns { get; set; } = null!;
    public SelectList SortOrders { get; set; } = null!;
    public SelectList PageSizes { get; set; } = null!;
    public MovieFilter? Filter { get; set; }
    public MovieSorting? Sorting { get; set; }

    public static MovieIndex Create(
        GetMoviesRequest request,
        PagedList<GetMoviesResponse> page,
        IReadOnlyList<GetGenresResponse> genres
    )
    {
        return new MovieIndex()
        {
            Movies = page.Items.Select(m => new MovieIndexItem(m)).ToList(),
            PagingMetadata = new(
                page.PageNumber,
                page.PageSize,
                page.TotalCount,
                page.TotalPages,
                page.HasPreviousPage,
                page.HasNextPage
            ),
            Genres = CreateGenreList(genres, request),
            SortColumns = CreateSortColumnsList(request),
            SortOrders = new SelectList(Enum.GetValues<SortOrder>(), selectedValue: request.SortOrder),
            PageSizes = new SelectList(PagingDefaults.PageSizeOptions, selectedValue: request.PageSize),
            Filter = new MovieFilter(request.SearchTerm, request.GenreName, request.StartPrice, request.EndPrice),
            Sorting = new MovieSorting(request.SortColumn, request.SortOrder),
        };
    }

    private static SelectList CreateGenreList(IReadOnlyList<GetGenresResponse> genres, GetMoviesRequest request)
    {
        return new SelectList(genres.Select(g => g.Name), selectedValue: request.GenreName);
    }

    private static SelectList CreateSortColumnsList(GetMoviesRequest request)
    {
        return new SelectList(
            Enum.GetValues<MovieSortColumn>()
                .Select(e => new
                {
                    Value = e,
                    Text = e switch
                    {
                        MovieSortColumn.GenreName => "Genre",
                        MovieSortColumn.ReleaseDate => "Release date",
                        _ => e.ToString(),
                    },
                }),
            "Value",
            "Text",
            selectedValue: request.SortColumn
        );
    }
}
