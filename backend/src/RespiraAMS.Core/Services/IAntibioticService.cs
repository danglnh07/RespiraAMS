using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.ServiceContract;

namespace RespiraAMS.Core.Services;

public interface IAntibioticService : IGenericService<AntibioticDtoRequest, AntibioticDtoResponse>
{
    
}