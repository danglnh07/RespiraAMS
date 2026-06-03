using BuildingBlocks.Dtos;
using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Mappers;

public class AntibioticSpectrumMapper : MapperBase,
    ICreateMapper<AntibioticSpectrum, CreateAntibioticSpectrumCommand>,
    IUpdateMapper<AntibioticSpectrum, UpdateAntibioticSpectrumCommand, UpdateAntibioticSpectrumResult>,
    IPagedMapper<GetPagedAntibioticSpectrumItem>
{
    public AntibioticSpectrum ToModel(CreateAntibioticSpectrumCommand command)
    {
        return new AntibioticSpectrum()
        {
            Name = command.Name,
            Description = command.Description,
        };
    }

    public AntibioticSpectrum ToModel(AntibioticSpectrum model, UpdateAntibioticSpectrumCommand command)
    {
        model.Name = command.Name;
        model.Description = command.Description;
        return model;
    }

    public UpdateAntibioticSpectrumResult ToResult(AntibioticSpectrum model)
    {
        return new UpdateAntibioticSpectrumResult
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
        };
    }

    public Pagination<GetPagedAntibioticSpectrumItem> ToPagination(IPagedList<GetPagedAntibioticSpectrumItem> items)
    {
        return new Pagination<GetPagedAntibioticSpectrumItem>(ToMetadata(items), items);
    }
}