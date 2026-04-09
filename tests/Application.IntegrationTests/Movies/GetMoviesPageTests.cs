using Application.Abstractions.Movies;
using Application.Common;
using Application.IntegrationTests.Core;
using Application.Movies.Pagination;
using Application.Movies.Queries;
using Application.Movies.Responses;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.IntegrationTests.Movies;

public class GetMoviesPageTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IMovieService _movieService => Scope.ServiceProvider.GetRequiredService<IMovieService>();

    [Fact]
    public async Task GetMoviesPage_ShouldReturnPaginatedMovies_WhenNoFilterApplied()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(1, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Count.ShouldBe(5);
        result.PageNumber.ShouldBe(1);
        result.PageSize.ShouldBe(5);
        result.TotalCount.ShouldBe(20);
        result.TotalPages.ShouldBe(4);
        result.HasPreviousPage.ShouldBeFalse();
        result.HasNextPage.ShouldBeTrue();
    }

    [Fact]
    public async Task GetMoviesPage_ShouldReturnCorrectSecondPage()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(2, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Count.ShouldBe(5);
        result.PageNumber.ShouldBe(2);
        result.HasPreviousPage.ShouldBeTrue();
        result.HasNextPage.ShouldBeTrue();
    }

    [Fact]
    public async Task GetMoviesPage_ShouldReturnLastPage_WithCorrectMetadata()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(4, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Count.ShouldBe(5);
        result.PageNumber.ShouldBe(4);
        result.HasPreviousPage.ShouldBeTrue();
        result.HasNextPage.ShouldBeFalse();
    }

    [Fact]
    public async Task GetMoviesPage_ShouldFilterBySearchTerm_WhenSearchTermProvided()
    {
        GetMoviesQuery query = new(
            new MovieFilter("Shawshank", null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(1, 10)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Count.ShouldBe(1);
        result.Items[0].Title.ShouldContain("Shawshank");
    }

    [Fact]
    public async Task GetMoviesPage_ShouldFilterByGenreName_WhenGenreNameProvided()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, "Sci-Fi", null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(1, 10)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Count.ShouldBe(4);
        result.Items.ShouldAllBe(m => m.GenreName == "Sci-Fi");
    }

    [Fact]
    public async Task GetMoviesPage_ShouldFilterByPriceRange_WhenStartAndEndPriceProvided()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, 10.00m, 12.00m),
            new MovieSorting(MovieSortColumn.Price, SortOrder.Ascending),
            new Paging(1, 20)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Count.ShouldBe(5);
        result.Items.ShouldAllBe(m => m.Price >= 10.00m && m.Price <= 12.00m);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldFilterByStartPrice_WhenOnlyStartPriceProvided()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, 12.00m, null),
            new MovieSorting(MovieSortColumn.Price, SortOrder.Ascending),
            new Paging(1, 20)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.ShouldAllBe(m => m.Price >= 12.00m);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldFilterByEndPrice_WhenOnlyEndPriceProvided()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, 10.00m),
            new MovieSorting(MovieSortColumn.Price, SortOrder.Ascending),
            new Paging(1, 20)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.ShouldAllBe(m => m.Price <= 10.00m);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldSortByTitleAscending_WhenTitleSortSelected()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(1, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Select(m => m.Title).ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldSortByTitleDescending_WhenTitleSortSelected()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Descending),
            new Paging(1, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Select(m => m.Title).ShouldBeInOrder(SortDirection.Descending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldSortByPriceAscending_WhenPriceSortSelected()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Price, SortOrder.Ascending),
            new Paging(1, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Select(m => m.Price).ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldSortByPriceDescending_WhenPriceSortSelected()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Price, SortOrder.Descending),
            new Paging(1, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Select(m => m.Price).ShouldBeInOrder(SortDirection.Descending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldSortByRatingDescending_WhenRatingSortSelected()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Rating, SortOrder.Descending),
            new Paging(1, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Select(m => m.Rating).ShouldBeInOrder(SortDirection.Descending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldSortByReleaseDateAscending_WhenReleaseDateSortSelected()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.ReleaseDate, SortOrder.Ascending),
            new Paging(1, 5)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Select(m => m.ReleaseDate).ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldSortByGenreName_WhenGenreNameSortSelected()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.GenreName, SortOrder.Ascending),
            new Paging(1, 20)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Select(m => m.GenreName).ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldReturnEmpty_WhenNoMoviesMatchFilter()
    {
        GetMoviesQuery query = new(
            new MovieFilter("NonExistentMovie12345", null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(1, 10)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.Count.ShouldBe(0);
        result.TotalCount.ShouldBe(0);
        result.TotalPages.ShouldBe(0);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldCombineFilterAndSort_Correctly()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, "Drama", 9.00m, 12.00m),
            new MovieSorting(MovieSortColumn.Price, SortOrder.Ascending),
            new Paging(1, 10)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        result.Items.ShouldAllBe(m => m.GenreName == "Drama" && m.Price >= 9.00m && m.Price <= 12.00m);
        result.Items.Select(m => m.Price).ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public async Task GetMoviesPage_ShouldReturnCorrectResponseProperties()
    {
        GetMoviesQuery query = new(
            new MovieFilter(null, null, null, null),
            new MovieSorting(MovieSortColumn.Title, SortOrder.Ascending),
            new Paging(1, 1)
        );

        PagedList<GetMoviesResponse> result = await _movieService.GetMoviesPage(query, CancellationToken.None);

        GetMoviesResponse movie = result.Items[0];
        movie.Id.ShouldNotBe(Guid.Empty);
        movie.Title.ShouldNotBeNullOrEmpty();
        movie.GenreName.ShouldNotBeNullOrEmpty();
        movie.ReleaseDate.ShouldBeGreaterThan(DateOnly.MinValue);
        movie.Price.ShouldBeGreaterThan(0);
        movie.Rating.ShouldBeInRange(1, 10);
    }
}
