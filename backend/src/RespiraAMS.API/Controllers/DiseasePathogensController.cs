using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Core.Dtos;
using RespiraAMS.Core.ServiceContract;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/disease-pathogens")]
public class DiseasePathogensController(IDiseasePathogenService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDiseasePathogen([FromBody] DiseasePathogenDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetDiseasePathogen), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<DiseasePathogenDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDiseasePathogen(Guid id)
    {
        return Ok(await service.GetByIdAsync(id));
    }

    [HttpGet]
    [ProducesResponseType<Pagination<DiseasePathogenDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDiseasePathogens([FromQuery] PaginationParam param)
    {
        var (metadata, diseasePathogens) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<DiseasePathogenDtoResponse>(metadata, diseasePathogens));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<DiseasePathogenDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDiseasePathogen(Guid id, [FromBody] DiseasePathogenDtoRequest req)
    {
        return Ok(await service.UpdateAsync(id, req));
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDiseasePathogen(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}