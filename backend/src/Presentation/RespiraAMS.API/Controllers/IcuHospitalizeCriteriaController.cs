using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.IcuHospitalizeCriteria.DeleteIcuHospitalizeCriterion;
using RespiraAMS.Application.Features.IcuHospitalizeCriteria.UpdateIcuHospitalizeCriterion;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/icu-hospitalize-criteria/{id:guid}")]
public class IcuHospitalizeCriteriaController(IMessageBus bus) : ControllerBase
{
    [HttpPut]
    public async Task<ApiResponse> UpdateIcuHospitalizeCriterion(Guid id,
        Guid icuId, [FromBody] UpdateIcuHospitalizeCriterionCommand request)
    {
        request.Id = icuId;
        // We don't really need disease ID, but we will still add them to the route
        // to make the API consistence
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    public async Task<ApiResponse> RemoveIcuHospitalizeCriterion(Guid id, Guid icuId)
    {
        // We don't really need disease ID, but we will still add them to the route
        // to make the API consistence
        await bus.InvokeAsync(new DeleteIcuHospitalizeCriterionCommand(icuId));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}