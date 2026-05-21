using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.ServiceContract;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/icu-criteria")]
public class IcuHospitalizeCriteriaController(IIcuHospitalizeCriterionService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateIcuHospitalizeCriterion(
        [FromBody] IcuHospitalizedCriterionDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetIcuHospitalizeCriterion), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<IcuHospitalizedCriterionDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetIcuHospitalizeCriterion(Guid id)
    {
        var criterion = await service.GetByIdAsync(id);
        return Ok(criterion);
    }

    [HttpGet]
    [ProducesResponseType<Pagination<IcuHospitalizedCriterionDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetIcuHospitalizeCriteria([FromQuery] PaginationParam param)
    {
        var (metadata, criteria) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<IcuHospitalizedCriterionDtoResponse>(metadata, criteria));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<IcuHospitalizedCriterionDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateIcuHospitalizeCriterion(Guid id, [FromBody] IcuHospitalizedCriterionDtoRequest req)
    {
        var result = await service.UpdateAsync(id, req);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteIcuHospitalizeCriterion(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}