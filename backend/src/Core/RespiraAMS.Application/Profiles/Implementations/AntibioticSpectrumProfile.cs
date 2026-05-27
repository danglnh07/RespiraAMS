using System.Collections.Generic;
using System.Linq;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Contracts;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Profiles.Implementations;

public class
    AntibioticSpectrumProfile : IProfile<AntibioticSpectrumDtoRequest, AntibioticSpectrum,
    AntibioticSpectrumDtoResponse>
{
    public AntibioticSpectrum ToModel(AntibioticSpectrumDtoRequest req)
    {
        return new AntibioticSpectrum()
        {
            Name = req.Name,
            Description = req.Description,
        };
    }

    public AntibioticSpectrum MapModel(AntibioticSpectrumDtoRequest req, AntibioticSpectrum model)
    {
        model.Name = req.Name;
        model.Description = req.Description;
        return model;
    }

    public AntibioticSpectrumDtoResponse ToResp(AntibioticSpectrum model)
    {
        return new AntibioticSpectrumDtoResponse()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
        };
    }

    public (PaginationMetadata, IEnumerable<AntibioticSpectrumDtoResponse>) ToPagedResponse(
        IPagedList<AntibioticSpectrum> models)
    {
        var metadata = IProfile<AntibioticSpectrumDtoRequest, AntibioticSpectrum, AntibioticSpectrumDtoResponse>
            .MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}