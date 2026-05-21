using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.Services;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AntibioticsController(IAntibioticService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAntibiotic([FromBody] AntibioticDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetAntibiotic), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<AntibioticDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAntibiotic(Guid id)
    {
        var resp = await service.GetByIdAsync(id);
        return Ok(resp);
    }

    [HttpGet]
    [ProducesResponseType<Pagination<AntibioticDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAntibiotics([FromQuery] PaginationParam param)
    {
        var (metadata, antibiotics) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<AntibioticDtoResponse>(metadata, antibiotics));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<AntibioticDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAntibiotic(Guid id, [FromBody] AntibioticDtoRequest req)
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
    public async Task<IActionResult> DeleteAntibiotic(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}