using System.Collections.Generic;
using System.Linq;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Contracts;
using RespiraAMS.Domain.Models;
using X.PagedList;

namespace RespiraAMS.Application.Profiles.Implementations;

public class PathogenProfile : IProfile<PathogenDtoRequest, Pathogen, PathogenDtoResponse>
{
    public Pathogen ToModel(PathogenDtoRequest req)
    {
        return new Pathogen()
        {
            Name = req.Name,
            Description = req.Description,
        };
    }

    public Pathogen MapModel(PathogenDtoRequest req, Pathogen model)
    {
        model.Name = req.Name;
        model.Description = req.Description;
        return model;
    }

    public PathogenDtoResponse ToResp(Pathogen model)
    {
        return new PathogenDtoResponse()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
        };
    }

    public (PaginationMetadata, IEnumerable<PathogenDtoResponse>) ToPagedResponse(IPagedList<Pathogen> models)
    {
        var metadata = IProfile<PathogenDtoRequest, Pathogen, PathogenDtoResponse>.MapPaginationMetadata(models);
        return (metadata, models.Select(ToResp));
    }
}