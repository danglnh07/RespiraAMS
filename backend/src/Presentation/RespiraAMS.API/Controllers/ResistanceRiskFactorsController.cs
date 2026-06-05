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
    public async Task<ApiResponse> UpdateResistanceRiskFactor(Guid id, Guid riskId,
        [FromBody] UpdateResistanceRiskFactorCommand request)
    {
        request.Id = riskId;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    public async Task<ApiResponse> RemoveResistanceRiskFactor(Guid id, Guid riskId)
    {
        // We don't really need disease ID, but we will still add them to the route
        // to make the API consistence
        await bus.InvokeAsync(new DeleteResistanceRiskFactorCommand(riskId));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}