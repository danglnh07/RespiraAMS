using RespiraAMS.Application.Abstracts.CQRS;

namespace RespiraAMS.Application.Features.IcuHospitalizeCriteria.DeleteIcuHospitalizeCriterion;

public class DeleteIcuHospitalizeCriterionCommand(Guid id) : ICommand
{
    public Guid Id { get; set; } = id;
}