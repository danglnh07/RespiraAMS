using BuildingBlocks.Dtos;
using X.PagedList;

namespace RespiraAMS.Application.Shared.Mappers;

public static class MapperBase
{
    private static PaginationMetadata ToMetadata<T>(IPagedList<T> items)
    {
        return new PaginationMetadata()
        {
            CurrentPage = items.PageNumber,
            HasNextPage = items.HasNextPage,
            HasPreviousPage = items.HasPreviousPage,
            PageCount = items.PageCount,
            PageSize = items.PageSize,
            TotalItemCount = items.TotalItemCount,
        };
    }

    public static Pagination<TItem> ToPagination<TItem>(IPagedList<TItem> items)
    {
        return new Pagination<TItem>(ToMetadata(items), items);
    }
}