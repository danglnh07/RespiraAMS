using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Models;
using X.PagedList;

namespace RespiraAMS.Core.Profiles;

public interface IProfile<in TReq, TModel, TResp>
{
    static PaginationMetadata MapPaginationMetadata(IPagedList<TModel> models)
    {
        return new PaginationMetadata
        {
            CurrentPage = models.PageNumber,
            HasNextPage = models.HasNextPage,
            HasPreviousPage = models.HasPreviousPage,
            PageCount = models.PageCount,
            PageSize = models.PageSize,
            TotalItemCount = models.TotalItemCount,
        };
    }
    TModel ToModel(TReq req);
    TModel MapModel(TReq req, TModel model);
    TResp ToResp(TModel model);
    (PaginationMetadata, IEnumerable<TResp>) ToPagedResponse(IPagedList<TModel> models);
}