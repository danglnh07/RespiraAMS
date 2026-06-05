using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.Antibiotics.CreateAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.DeleteAntibiotic;
using RespiraAMS.Application.Features.Antibiotics.GetAntibiotics;
using RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotics;
using PagedAntibioticItem = RespiraAMS.Application.Features.Antibiotics.GetPagedAntibiotics.AntibioticItem;
using AntibioticItem = RespiraAMS.Application.Features.Antibiotics.GetAntibiotics.AntibioticItem;
using RespiraAMS.Application.Features.Antibiotics.UpdateAntibiotic;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/antibiotics")]
public class AntibioticsController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse<CreateAntibioticResult>> CreateAntibiotic([FromBody] CreateAntibioticCommand request)
    {
        var result = await bus.InvokeAsync<CreateAntibioticResult>(request);
        return ApiResponse<CreateAntibioticResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ApiResponse<Pagination<PagedAntibioticItem>>> GetAntibiotics(
        [FromQuery] GetPagedAntibioticsQuery request)
    {
        var result = await bus.InvokeAsync<Pagination<PagedAntibioticItem>>(request);
        return ApiResponse<Pagination<PagedAntibioticItem>>.Ok(result);
    }

    [HttpGet]
    [Route("list")]
    public async Task<ApiResponse<IEnumerable<AntibioticItem>>> GetAntibiotics()
    {
        var result = await bus.InvokeAsync<IEnumerable<AntibioticItem>>(new GetAntibioticsQuery());
        return ApiResponse<IEnumerable<AntibioticItem>>.Ok(result);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ApiResponse> UpdateAntibiotic(Guid id,
        [FromBody] UpdateAntibioticCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ApiResponse> DeleteAntibiotic(Guid id)
    {
        await bus.InvokeAsync(new DeleteAntibioticCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}