using BuildingBlocks.Dtos;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Mappers;

public class AntibioticMapper : MapperBase, 
    ICreateMapper<Antibiotic, CreateAntibioticCommand>,
    IUpdateMapper<Antibiotic, UpdateAntibioticCommand, UpdateAntibioticResult>,
    IPagedMapper<GetPagedAntibioticItem>
{
    public Antibiotic ToModel(CreateAntibioticCommand command)
    {
        return new Antibiotic()
        {
            Name = command.Name,
            AntibioticSpectrumId = command.AntibioticSpectrumId,
            Category = command.Category,
            RouteOfAdministrations = command.RouteOfAdministrations,
            Dosages = command.Dosages,
        };
    }

    public Antibiotic ToModel(Antibiotic model, UpdateAntibioticCommand command)
    {
        model.Name = command.Name;
        model.AntibioticSpectrumId = command.AntibioticSpectrumId;
        model.Category = command.Category;
        model.RouteOfAdministrations = command.RouteOfAdministrations;
        model.Dosages = command.Dosages;
        return model;
    }

    public UpdateAntibioticResult ToResult(Antibiotic model)
    {
        return new UpdateAntibioticResult
        {
            Id = model.Id,
            Name = model.Name,
            AntibioticSpectrumId = model.AntibioticSpectrumId,
            Category = model.Category,
            RouteOfAdministrations = model.RouteOfAdministrations,
            Dosages = model.Dosages,
        };
    }

    public Pagination<GetPagedAntibioticItem> ToPagination(IPagedList<GetPagedAntibioticItem> items)
    {
        return new Pagination<GetPagedAntibioticItem>(ToMetadata(items), items);
    }
}