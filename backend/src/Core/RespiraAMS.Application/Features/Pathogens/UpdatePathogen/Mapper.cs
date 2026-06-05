using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Pathogens.UpdatePathogen;

public class UpdatePathogenMapper : IUpdateMapper<Pathogen, UpdatePathogenCommand>
{
    public void MapModel(Pathogen model, UpdatePathogenCommand command)
    {
        model.Name = command.Name;
        model.Description = command.Description;
        model.UpdatedAt = DateTimeOffset.UtcNow;
    }
}