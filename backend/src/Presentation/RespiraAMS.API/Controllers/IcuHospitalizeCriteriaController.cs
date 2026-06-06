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
        [FromBody] UpdateIcuHospitalizeCriterionCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    public async Task<ApiResponse> RemoveIcuHospitalizeCriterion(Guid id)
    {
        await bus.InvokeAsync(new DeleteIcuHospitalizeCriterionCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}