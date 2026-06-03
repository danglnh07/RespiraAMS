namespace RespiraAMS.Application.Features.Antibiotics.DeleteAntibiotic;

public class DeleteAntibioticCommand(Guid id)
{
    public Guid Id { get; set; } = id;
}