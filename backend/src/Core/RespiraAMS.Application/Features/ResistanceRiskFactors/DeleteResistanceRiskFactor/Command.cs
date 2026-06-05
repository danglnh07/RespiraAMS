using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.ResistanceRiskFactors.DeleteResistanceRiskFactor;

public class DeleteResistanceRiskFactorCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}