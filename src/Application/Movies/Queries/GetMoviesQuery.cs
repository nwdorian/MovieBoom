using Application.Common;
using Application.Movies.Pagination;

namespace Application.Movies.Queries;

public record class GetMoviesQuery(MovieFilter Filter, MovieSorting Sorting, Paging Paging);
