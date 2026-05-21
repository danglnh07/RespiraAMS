using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Services.Contracts;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PathogensController(IPathogenService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePathogen([FromBody] PathogenDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetPathogen), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<PathogenDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPathogen(Guid id)
    {
        var pathogen = await service.GetByIdAsync(id);
        return Ok(pathogen);
    }

    [HttpGet]
    [ProducesResponseType<Pagination<PathogenDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPathogens([FromQuery] PaginationParam param)
    {
        var (metadata, pathogens) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<PathogenDtoResponse>(metadata, pathogens));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<PathogenDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePathogen(Guid id, [FromBody] PathogenDtoRequest req)
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
    public async Task<IActionResult> DeletePathogen(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}