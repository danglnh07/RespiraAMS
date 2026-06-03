using BuildingBlocks.Dtos;
using X.PagedList;

namespace RespiraAMS.Application.Mappers;

public class MapperBase
{
    protected PaginationMetadata ToMetadata<T>(IPagedList<T> items)
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
}