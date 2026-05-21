using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Services.Contracts;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiseasesController(IDiseaseService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDisease([FromBody] DiseaseDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetDisease), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<DiseaseDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDisease(Guid id)
    {
        var disease = await service.GetByIdAsync(id);
        return Ok(disease);
    }

    [HttpGet]
    [ProducesResponseType<Pagination<DiseaseDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDiseases([FromQuery] PaginationParam param)
    {
        var (metadata, diseases) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<DiseaseDtoResponse>(metadata, diseases));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<DiseaseDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDisease(Guid id, [FromBody] DiseaseDtoRequest req)
    {
        var disease = await service.UpdateAsync(id, req);
        return Ok(disease);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDisease(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}