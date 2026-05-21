using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Services.Contracts;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/resistance-risks")]
public class ResistanceRiskFactorsController(IResistanceRiskFactorService service)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAntibioticResistanceInfectionCriteria([FromBody] ResistanceRiskFactorDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetAntibioticResistanceInfectionCriterion), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<ResistanceRiskFactorDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAntibioticResistanceInfectionCriterion(Guid id)
    {
        var result = await service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType<Pagination<ResistanceRiskFactorDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAntibioticResistanceInfectionCriteria([FromQuery] PaginationParam param)
    {
        var (metadata, criteria) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<ResistanceRiskFactorDtoResponse>(metadata, criteria));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<ResistanceRiskFactorDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAntibioticResistanceInfectionCriteria(Guid id, ResistanceRiskFactorDtoRequest req)
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
    public async Task<IActionResult> DeleteAntibioticResistanceInfectionCriteria(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}