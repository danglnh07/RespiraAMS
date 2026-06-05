using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.AntibioticSpectra.CreateAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.DeleteAntibioticSpectrum;
using RespiraAMS.Application.Features.AntibioticSpectra.GetAntibioticSpectra;
using RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectra;
using PagedAntibioticSpectrumItem = RespiraAMS.Application.Features.AntibioticSpectra.GetPagedAntibioticSpectra.AntibioticSpectrumItem;
using AntibioticSpectrumItem = RespiraAMS.Application.Features.AntibioticSpectra.GetAntibioticSpectra.AntibioticSpectrumItem;
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
        return ApiResponse<CreateAntibioticSpectrumResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }
    
    [HttpGet]
    public async Task<ApiResponse<Pagination<PagedAntibioticSpectrumItem>>> GetAntibioticSpectra(
        [FromQuery] GetPagedAntibioticSpectraQuery query)
    {
        var result = await bus.InvokeAsync<Pagination<PagedAntibioticSpectrumItem>>(query);
        return ApiResponse<Pagination<PagedAntibioticSpectrumItem>>.Ok(result);
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<ApiResponse<IEnumerable<AntibioticSpectrumItem>>> GetAntibioticSpectra()
    {
        var result = await bus.InvokeAsync<IEnumerable<AntibioticSpectrumItem>>(new GetAntibioticSpectraQuery());
        return ApiResponse<IEnumerable<AntibioticSpectrumItem>>.Ok(result);
    }
    
    [HttpPut]
    [Route("/api/antibiotic-spectra/{id:guid}")]
    public async Task<ApiResponse> UpdateAntibioticSpectrum(
        Guid id, [FromBody] UpdateAntibioticSpectrumCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
    
    [HttpDelete]
    [Route("/api/antibiotic-spectra/{id:guid}")]
    public async Task<ApiResponse> DeleteAntibioticSpectrum(Guid id)
    {
        await bus.InvokeAsync(new DeleteAntibioticSpectrumCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }
}