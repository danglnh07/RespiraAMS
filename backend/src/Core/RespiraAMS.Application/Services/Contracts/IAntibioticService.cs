using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Services.Contracts;

public interface IAntibioticService : IGenericService<AntibioticDtoRequest, AntibioticDtoResponse>
{
    
}