using RespiraAMS.Application.Abstracts.Mappers;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;

public class CreateAntibioticSpectrumMapper : ICreateMapper<AntibioticSpectrum, CreateAntibioticSpectrumCommand>
{
    public AntibioticSpectrum ToModel(CreateAntibioticSpectrumCommand command)
    {
        return new AntibioticSpectrum()
        {
            Name = command.Name,
            Description = command.Description,
        };
    }
}