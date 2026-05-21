using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Models;
using RespiraAMS.Core.Profiles;
using X.PagedList;

namespace RespiraAMS.Infrastructure.Profiles;

public class AntibioticSpectrumProfile : IProfile<AntibioticSpectrumDtoRequest, AntibioticSpectrum, AntibioticSpectrumDtoResponse>
{
    public AntibioticSpectrum ToModel(AntibioticSpectrumDtoRequest req)
    {
        return new AntibioticSpectrum()
        {
            Name = req.Name,
        };
    }

    public AntibioticSpectrum MapModel(AntibioticSpectrumDtoRequest req, AntibioticSpectrum model)
    {
        model.Name = req.Name;
        return model;
    }

    public AntibioticSpectrumDtoResponse ToResp(AntibioticSpectrum model)
    {
        return new AntibioticSpectrumDtoResponse()
        {
            Id = model.Id,
            Name = model.Name,
        };
    }

    public (PaginationMetadata, IEnumerable<AntibioticSpectrumDtoResponse>) ToPagedResponse(IPagedList<AntibioticSpectrum> models)
    {
        var metadata = IProfile<AntibioticSpectrumDtoRequest, AntibioticSpectrum, AntibioticSpectrumDtoResponse>.MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}