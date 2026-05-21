using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Models;
using RespiraAMS.Core.Profiles;
using X.PagedList;

namespace RespiraAMS.Infrastructure.Profiles;

public class AntibioticProfile(AntibioticSpectrumProfile spectrumProfile) : IProfile<AntibioticDtoRequest, Antibiotic, AntibioticDtoResponse>
{
    public Antibiotic ToModel(AntibioticDtoRequest req)
    {
        return new Antibiotic()
        {
            Name = req.Name,
            AntibioticSpectrumId = req.AntibioticSpectrumId,
            Category = req.Category,  
            RouteOfAdministrations = req.RouteOfAdministrations,
            Dosages =  req.Dosages,
        };
    }

    public Antibiotic MapModel(AntibioticDtoRequest req, Antibiotic model)
    {
        model.Name = req.Name;
        model.AntibioticSpectrumId = req.AntibioticSpectrumId;
        model.Category = req.Category;
        model.RouteOfAdministrations = req.RouteOfAdministrations;
        model.Dosages = req.Dosages;
        return model;
    }

    public AntibioticDtoResponse ToResp(Antibiotic model)
    {
        return new AntibioticDtoResponse()
        {
            Id = model.Id,
            Name = model.Name,
            Category = model.Category,
            AntibioticSpectrum = spectrumProfile.ToResp(model.AntibioticSpectrum),
            RouteOfAdministrations = model.RouteOfAdministrations,
            Dosages = model.Dosages
        };
    }

    public (PaginationMetadata, IEnumerable<AntibioticDtoResponse>) ToPagedResponse(IPagedList<Antibiotic> models)
    {
        var metadata = IProfile<AntibioticDtoRequest, Antibiotic, AntibioticDtoResponse>.MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}