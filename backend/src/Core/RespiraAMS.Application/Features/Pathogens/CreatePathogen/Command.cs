using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Pathogens.CreatePathogen;

public class CreatePathogenCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CreatePathogenResult(Guid id)
{
    public Guid Id { get; set; } = id;
}