using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Services.Contracts;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnoseController(IDiagnoseService service) : ControllerBase
{
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<DiagnosisTemplateDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDiagnosisTemplate([Description("Disease ID")] Guid id)
    {
        var result = await service.GetDiagnosisTemplate(id);
        return Ok(result);
    }

    [HttpPost]
    [Route("{id:guid}")]
    [ProducesResponseType<DiagnosisResultDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Diagnose([Description("Disease ID")] Guid id, [FromBody] ClinicalPictureDto req)
    {
        var result = await service.Diagnose(id, req);
        return Ok(result);
    }

    [HttpPost]
    [Route("{id:guid}/recommend")]
    [ProducesResponseType<IEnumerable<TreatmentProtocolDtoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Recommend([Description("Disease ID")] Guid id, [FromBody] RecommendDtoRequest req)
    {
        var result = await service.Recommend(id, req);
        return Ok(result);
    }
}