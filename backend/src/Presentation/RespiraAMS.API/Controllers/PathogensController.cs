using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.Pathogens.CreatePathogen;
using RespiraAMS.Application.Features.Pathogens.DeletePathogen;
using RespiraAMS.Application.Features.Pathogens.GetPagedPathogens;
using RespiraAMS.Application.Features.Pathogens.GetPathogens;
using PagedPathogenItem = RespiraAMS.Application.Features.Pathogens.GetPagedPathogens.PathogenItem;
using PathogenItem = RespiraAMS.Application.Features.Pathogens.GetPathogens.PathogenItem;
using RespiraAMS.Application.Features.Pathogens.UpdatePathogen;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/pathogens")]
public class PathogensController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse<CreatePathogenResult>> CreatePathogen([FromBody] CreatePathogenCommand request)
    {
        var result = await bus.InvokeAsync<CreatePathogenResult>(request);
        return ApiResponse<CreatePathogenResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ApiResponse<Pagination<PagedPathogenItem>>> GetPathogens(
        [FromQuery] GetPagedPathogensQuery query)
    {
        var result = await bus.InvokeAsync<Pagination<PagedPathogenItem>>(query);
        return ApiResponse<Pagination<PagedPathogenItem>>.Ok(result);
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<ApiResponse<IEnumerable<PathogenItem>>> GetPathogens()
    {
        var result = await bus.InvokeAsync<IEnumerable<PathogenItem>>(new GetPathogensQuery());
        return ApiResponse<IEnumerable<PathogenItem>>.Ok(result);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ApiResponse> UpdatePathogen(Guid id,
        [FromBody] UpdatePathogenCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ApiResponse> DeletePathogen(Guid id)
    {
        await bus.InvokeAsync(new DeletePathogenCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}