using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Pathogens.DeletePathogen;

public class DeletePathogenCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}