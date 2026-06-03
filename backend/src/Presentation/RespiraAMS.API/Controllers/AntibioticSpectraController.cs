using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.UpdateAntibioticSpectrum;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/antibiotic-spectra")]
public class AntibioticSpectraController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse<CreateAntibioticSpectrumResult>> CreateAntibioticSpectrum(
        [FromBody] CreateAntibioticSpectrumCommand request)
    {
        var result = await bus.InvokeAsync<CreateAntibioticSpectrumResult>(request);
        return ApiResponse<CreateAntibioticSpectrumResult>.Ok(result, statusCode: 201);
    }

    [HttpGet]
    public async Task<ApiResponse<Pagination<GetPagedAntibioticSpectrumItem>>> GetAntibioticSpectrum(
        [FromQuery] GetPagedAntibioticSpectrumQuery query)
    {
        var result = await bus.InvokeAsync<Pagination<GetPagedAntibioticSpectrumItem>>(query);
        return ApiResponse<Pagination<GetPagedAntibioticSpectrumItem>>.Ok(result);
    }

    [HttpPut]
    [Route("/api/antibiotic-spectra/{id:guid}")]
    public async Task<ApiResponse<UpdateAntibioticSpectrumResult>> UpdateAntibioticSpectrum(
        Guid id, [FromBody] UpdateAntibioticSpectrumCommand request)
    {
        request.Id = id;
        var result = await bus.InvokeAsync<UpdateAntibioticSpectrumResult>(request);
        return ApiResponse<UpdateAntibioticSpectrumResult>.Ok(result);
    }

    [HttpDelete]
    [Route("/api/antibiotic-spectra/{id:guid}")]
    public async Task<ApiResponse> DeleteAntibioticSpectrum(Guid id)
    {
        await bus.InvokeAsync(new DeleteAntibioticSpectrumCommand() { Id = id });
        return ApiResponse.Ok(statusCode: 204);
    }
}