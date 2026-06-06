using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.ResistanceRiskFactors.DeleteResistanceRiskFactor;
using RespiraAMS.Application.Features.ResistanceRiskFactors.UpdateResistanceRiskFactor;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/resistance-risk-factors/{id:guid}")]
public class ResistanceRiskFactorsController(IMessageBus bus) : ControllerBase
{
    [HttpPut]
    public async Task<ApiResponse> UpdateResistanceRiskFactor(Guid id,
        [FromBody] UpdateResistanceRiskFactorCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    public async Task<ApiResponse> RemoveResistanceRiskFactor(Guid id)
    {
        await bus.InvokeAsync(new DeleteResistanceRiskFactorCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}