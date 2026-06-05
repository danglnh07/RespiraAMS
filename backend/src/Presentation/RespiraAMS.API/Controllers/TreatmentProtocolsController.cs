using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.TreatmentProtocols.AddNewCriteria;
using RespiraAMS.Application.Features.TreatmentProtocols.DeleteTreatmentProtocol;
using RespiraAMS.Application.Features.TreatmentProtocols.GetTreatmentProtocolById;
using RespiraAMS.Application.Features.TreatmentProtocols.UpdateTreatmentProtocol;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/treatment-protocols/{id:guid}")]
public class TreatmentProtocolsController(IMessageBus bus) : ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<TreatmentProtocolResult>> GetProtocol(Guid id)
    {
        var result = await bus.InvokeAsync<TreatmentProtocolResult>(new GetTreatmentProtocolByIdQuery(id));
        return ApiResponse<TreatmentProtocolResult>.Ok(result);
    }

    [HttpPut]
    public async Task<ApiResponse> UpdateProtocol(Guid id, [FromBody] UpdateTreatmentProtocolCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpPut]
    [Route("criteria")]
    public async Task<ApiResponse> AddNewCriteria(Guid id, [FromBody] AddNewCriteriaCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    public async Task<ApiResponse> DeleteTreatmentProtocol(Guid id)
    {
        await bus.InvokeAsync(new DeleteTreatmentProtocolCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}