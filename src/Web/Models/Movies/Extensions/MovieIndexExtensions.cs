using System.Globalization;

namespace Web.Models.Movies.Extensions;

public static class MovieIndexExtensions
{
    public static Dictionary<string, string?> ToPagingRouteValues(this MovieIndex model)
    {
        return new()
        {
            [nameof(model.Filter.SearchTerm)] = model?.Filter?.SearchTerm,
            [nameof(model.Filter.GenreName)] = model?.Filter?.GenreName,
            [nameof(model.Filter.StartPrice)] = model?.Filter?.StartPrice?.ToString(CultureInfo.InvariantCulture),
            [nameof(model.Filter.EndPrice)] = model?.Filter?.EndPrice?.ToString(CultureInfo.InvariantCulture),
            [nameof(model.Sorting.SortColumn)] = model?.Sorting?.SortColumn.ToString(),
            [nameof(model.Sorting.SortOrder)] = model?.Sorting?.SortOrder.ToString(),
            [nameof(model.PagingMetadata.PageSize)] = model?.PagingMetadata?.PageSize.ToString(
                CultureInfo.InvariantCulture
            ),
        };
    }
}
