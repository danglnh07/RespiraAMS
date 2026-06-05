using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.Antibiotics.DeleteAntibiotic;

public class DeleteAntibioticCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}