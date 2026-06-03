using BuildingBlocks.Dtos;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotic;
using RespiraAMS.Application.Features.Pathogens.CreatePathogen;
using RespiraAMS.Application.Features.Pathogens.GetPagedPathogen;
using RespiraAMS.Application.Features.Pathogens.UpdatePathogen;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Mappers;

public class PathogenMapper : MapperBase, 
    ICreateMapper<Pathogen, CreatePathogenCommand>, 
    IUpdateMapper<Pathogen, UpdatePathogenCommand, UpdatePathogenResult>,
    IPagedMapper<GetPagedPathogenItem>
{
    public Pathogen ToModel(CreatePathogenCommand command)
    {
        return new Pathogen()
        {
            Name = command.Name,
            Description = command.Description
        };
    }

    public Pathogen ToModel(Pathogen model, UpdatePathogenCommand command)
    {
        model.Name = command.Name;
        model.Description = command.Description;
        return model;
    }

    public UpdatePathogenResult ToResult(Pathogen model)
    {
        return new UpdatePathogenResult()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
    }

    public Pagination<GetPagedPathogenItem> ToPagination(IPagedList<GetPagedPathogenItem> items)
    {
        return new Pagination<GetPagedPathogenItem>(ToMetadata(items), items);
    }
}