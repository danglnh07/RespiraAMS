using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.DiseasePathogens.DeleteDiseasePathogen;

public class DeleteDiseasePathogenCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}