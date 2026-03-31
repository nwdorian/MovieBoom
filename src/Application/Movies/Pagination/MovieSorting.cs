using Application.Common;

namespace Application.Movies.Pagination;

public record class MovieSorting(MovieSortColumn SortColumn, SortOrder SortOrder);
