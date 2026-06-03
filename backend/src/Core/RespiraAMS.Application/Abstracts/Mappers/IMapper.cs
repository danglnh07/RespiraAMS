using BuildingBlocks.Dtos;
using X.PagedList;

namespace RespiraAMS.Application.Abstracts.Mappers;

public interface ICreateMapper<out TModel, in TCreateCommand>
{
    TModel ToModel(TCreateCommand command);
}

public interface IUpdateMapper<TModel, in TUpdateCommand, out TUpdateResult>
{
    TModel ToModel(TModel model, TUpdateCommand command);
    TUpdateResult ToResult(TModel model);
}

public interface IPagedMapper<TItem>
{
    Pagination<TItem> ToPagination(IPagedList<TItem> items);
}
