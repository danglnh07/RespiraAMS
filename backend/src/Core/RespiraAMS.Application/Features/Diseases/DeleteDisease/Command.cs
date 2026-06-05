using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Diseases.DeleteDisease;

public class DeleteDiseaseCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}