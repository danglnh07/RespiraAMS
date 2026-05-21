using RespiraAMS.Application.Dtos;

namespace RespiraAMS.Application.Services.Contracts;

public interface IDiseaseService : IGenericService<DiseaseDtoRequest, DiseaseDtoResponse>
{
    
}