using API.Common.Models;

namespace API.Common.Extensions;

public static class PaginationMappingExtensions
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, PaginationRequest req, CancellationToken cancellationToken)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, req.PageNumber, req.PageSize);
    }
}