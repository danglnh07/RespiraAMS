using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.DiseasePathogens.DeleteDiseasePathogen;
using RespiraAMS.Application.Features.DiseasePathogens.UpdateDiseasePathogen;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/causes/{id:guid}")]
public class DiseasePathogensController(IMessageBus bus) : ControllerBase
{
    [HttpPut]
    public async Task<ApiResponse> UpdateDiseasePathogen(Guid id, [FromBody] UpdateDiseasePathogenCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    public async Task<ApiResponse> RemoveDiseasePathogen(Guid id)
    {
        // We don't really need disease ID, but we will still add them to the route
        // to make the API consistence
        await bus.InvokeAsync(new DeleteDiseasePathogenCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}