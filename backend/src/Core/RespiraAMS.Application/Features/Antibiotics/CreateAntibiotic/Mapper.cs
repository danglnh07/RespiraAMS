using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;

public class CreateAntibioticMapper : ICreateMapper<Antibiotic, CreateAntibioticCommand>
{
    public Antibiotic ToModel(CreateAntibioticCommand command)
    {
        return new Antibiotic()
        {
            Name = command.Name,
            AntibioticSpectrumId = command.AntibioticSpectrumId,
            Category = command.Category,
            Dosages = command.Dosages,
        };
    }
}