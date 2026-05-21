using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Services.Contracts;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/antibiotic-spectra")]
public class AntibioticSpectraController(IAntibioticSpectrumService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAntiBioticSpectrum([FromBody] AntibioticSpectrumDtoRequest req)
    {
        var id = await service.CreateAsync(req);
        return CreatedAtAction(nameof(GetAntibioticSpectrum), new { id }, id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<AntibioticDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAntibioticSpectrum(Guid id)
    {
        var resp = await service.GetByIdAsync(id);
        return Ok(resp);
    }

    [HttpGet]
    [ProducesResponseType<Pagination<AntibioticSpectrumDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAntibioticSpectra([FromQuery] PaginationParam param)
    {
        var (metadata, spectra) = await service.GetListAsync(param.Page, param.Size);
        return Ok(new Pagination<AntibioticSpectrumDtoResponse>(metadata, spectra));
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType<AntibioticSpectrumDtoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAntibioticSpectrum(Guid id, [FromBody] AntibioticSpectrumDtoRequest req)
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
    public async Task<IActionResult> DeleteAntibioticSpectrum(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}