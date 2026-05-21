using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Services;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/treatment-protocols")]
public class TreatmentProtocolsController(ITreatmentProtocolService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTreatmentProtocol([FromBody] TreatmentProtocolDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetTreatmentProtocol), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<TreatmentProtocolDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTreatmentProtocol(Guid id)
    {
        var resp = await service.GetByIdAsync(id);
        return Ok(resp);
    }

    [HttpGet]
    [ProducesResponseType<Pagination<TreatmentProtocolDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTreatmentProtocols([FromQuery] PaginationParam param)
    {
        var (metadata, protocols) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<TreatmentProtocolDtoResponse>(metadata, protocols));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<TreatmentProtocolDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTreatmentProtocol(Guid id, [FromBody] TreatmentProtocolDtoRequest req)
    {
        var resp = await service.UpdateAsync(id, req);
        return Ok(resp);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTreatmentProtocol(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    [Route("{id:guid}/criteria")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCriteria(Guid id, [FromBody] List<CriterionDtoRequest> req)
    {
        await service.AddCriteriaToProtocol(id, req);
        return Created();
    }
}
