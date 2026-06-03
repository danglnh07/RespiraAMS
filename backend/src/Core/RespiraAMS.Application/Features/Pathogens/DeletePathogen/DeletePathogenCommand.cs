namespace RespiraAMS.Application.Features.Pathogens.DeletePathogen;

public class DeletePathogenCommand(Guid id)
{
    public Guid Id { get; set; } = id;
}