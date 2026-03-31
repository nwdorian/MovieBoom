using Application.Common;
using Application.Movies.Pagination;
using Web.Constants;

namespace Web.Models.Movies.Requests;

public record class GetMoviesRequest(
    string? SearchTerm = null,
    string? GenreName = null,
    decimal? StartPrice = null,
    decimal? EndPrice = null,
    MovieSortColumn SortColumn = MovieSortColumn.Title,
    SortOrder SortOrder = SortOrder.Ascending,
    int PageNumber = PagingDefaults.PageNumber,
    int PageSize = PagingDefaults.PageSize
);
