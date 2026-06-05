using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.TreatmentProtocols.DeleteTreatmentProtocol;

public class DeleteTreatmentProtocolCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}