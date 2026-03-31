using System.Linq.Expressions;
using Application.Common;
using Domain.Movies;

namespace Application.Movies.Pagination;

public static class MoviesQueryExtensions
{
    public static IQueryable<Movie> ApplyFiltering(this IQueryable<Movie> query, MovieFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            query = query.Where(m => m.Title.Contains(filter.SearchTerm));
        }

        if (!string.IsNullOrWhiteSpace(filter.GenreName))
        {
            query = query.Where(m => m.Genre.Name == filter.GenreName);
        }

        if (filter.StartPrice is not null)
        {
            query = query.Where(m => m.Price >= filter.StartPrice);
        }

        if (filter.EndPrice is not null)
        {
            query = query.Where(m => m.Price <= filter.EndPrice);
        }

        return query;
    }

    public static IQueryable<Movie> ApplySorting(this IQueryable<Movie> query, MovieSorting sorting)
    {
        if (sorting.SortOrder == SortOrder.Descending)
        {
            query = query.OrderByDescending(GetSortColumn(sorting));
        }
        else
        {
            query = query.OrderBy(GetSortColumn(sorting));
        }

        return query;
    }

    private static Expression<Func<Movie, object>> GetSortColumn(MovieSorting sorting)
    {
        return sorting.SortColumn switch
        {
            MovieSortColumn.GenreName => movie => movie.Genre.Name,
            MovieSortColumn.Price => movie => movie.Price,
            MovieSortColumn.Rating => movie => movie.Rating,
            MovieSortColumn.ReleaseDate => movie => movie.ReleaseDate,
            _ => movie => movie.Title,
        };
    }
}
