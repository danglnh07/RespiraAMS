using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.Pathogens.CreatePathogen;
using RespiraAMS.Application.Features.Pathogens.DeletePathogen;
using RespiraAMS.Application.Features.Pathogens.GetPagedPathogen;
using RespiraAMS.Application.Features.Pathogens.UpdatePathogen;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PathogensController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse<CreatePathogenResult>> CreatePathogen([FromBody] CreatePathogenCommand request)
    {
        var result = await bus.InvokeAsync<CreatePathogenResult>(request);
        return ApiResponse<CreatePathogenResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ApiResponse<Pagination<GetPagedPathogenItem>>> GetPathogens(
        [FromQuery] GetPagedPathogenQuery query)
    {
        var result = await bus.InvokeAsync<Pagination<GetPagedPathogenItem>>(query);
        return ApiResponse<Pagination<GetPagedPathogenItem>>.Ok(result);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ApiResponse<UpdatePathogenResult>> UpdatePathogen(Guid id,
        [FromBody] UpdatePathogenCommand request)
    {
        request.Id = id;
        var result = await bus.InvokeAsync<UpdatePathogenResult>(request);
        return ApiResponse<UpdatePathogenResult>.Ok(result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ApiResponse> DeletePathogen(Guid id)
    {
        await bus.InvokeAsync(new DeletePathogenCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}