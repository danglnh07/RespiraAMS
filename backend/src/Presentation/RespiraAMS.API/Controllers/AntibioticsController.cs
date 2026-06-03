using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.DeleteAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AntibioticsController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse<CreateAntibioticResult>> CreateAntibiotic([FromBody] CreateAntibioticCommand request)
    {
        var result = await bus.InvokeAsync<CreateAntibioticResult>(request);
        return ApiResponse<CreateAntibioticResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ApiResponse<Pagination<GetPagedAntibioticItem>>> GetAntibiotics(
        [FromQuery] GetPagedAntibioticQuery request)
    {
        var result = await bus.InvokeAsync<Pagination<GetPagedAntibioticItem>>(request);
        return ApiResponse<Pagination<GetPagedAntibioticItem>>.Ok(result);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ApiResponse<UpdateAntibioticResult>> UpdateAntibiotic(Guid id,
        [FromBody] UpdateAntibioticCommand request)
    {
        request.Id = id;
        var result = await bus.InvokeAsync<UpdateAntibioticResult>(request);
        return ApiResponse<UpdateAntibioticResult>.Ok(result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ApiResponse> DeleteAntibiotic(Guid id)
    {
        await bus.InvokeAsync(new DeleteAntibioticCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}