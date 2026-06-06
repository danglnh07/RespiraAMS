using BuildingBlocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using RespiraAMS.Application.Features.DiseasePathogens.CreateDiseasePathogen;
using RespiraAMS.Application.Features.Diseases.CreateDisease;
using RespiraAMS.Application.Features.Diseases.DeleteDisease;
using RespiraAMS.Application.Features.Diseases.GetDiagnosisTemplate;
using RespiraAMS.Application.Features.Diseases.GetDiseaseById;
using RespiraAMS.Application.Features.Diseases.GetDiseases;
using RespiraAMS.Application.Features.Diseases.GetPagedDiseases;
using PagedDiseaseItem = RespiraAMS.Application.Features.Diseases.GetPagedDiseases.DiseaseItem;
using DiseaseItem = RespiraAMS.Application.Features.Diseases.GetDiseases.DiseaseItem;
using RespiraAMS.Application.Features.Diseases.UpdateDisease;
using RespiraAMS.Application.Features.IcuHospitalizeCriteria.CreateIcuHospitalizeCriterion;
using RespiraAMS.Application.Features.ResistanceRiskFactors.CreateResistanceRiskFactor;
using RespiraAMS.Application.Features.TreatmentProtocols.CreateTreatmentProtocol;
using Wolverine;

namespace RespiraAMS.API.Controllers;

[ApiController]
[Route("api/diseases")]
public class DiseasesController(IMessageBus bus) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse<CreateDiseaseResult>> CreateDisease([FromBody] CreateDiseaseCommand request)
    {
        var result = await bus.InvokeAsync<CreateDiseaseResult>(request);
        return ApiResponse<CreateDiseaseResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ApiResponse<Pagination<PagedDiseaseItem>>> GetDiseases(
        [FromQuery] GetPagedDiseasesQuery request)
    {
        var result = await bus.InvokeAsync<Pagination<PagedDiseaseItem>>(request);
        return ApiResponse<Pagination<PagedDiseaseItem>>.Ok(result);
    }

    [HttpGet]
    [Route("list")]
    public async Task<ApiResponse<IEnumerable<DiseaseItem>>> GetDiseases()
    {
        var result = await bus.InvokeAsync<IEnumerable<DiseaseItem>>(new GetDiseasesQuery());
        return ApiResponse<IEnumerable<DiseaseItem>>.Ok(result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ApiResponse<DiseaseResult>> GetDisease(Guid id)
    {
        var result = await bus.InvokeAsync<DiseaseResult>(new GetDiseaseByIdQuery(id));
        return ApiResponse<DiseaseResult>.Ok(result);
    }

    [HttpGet]
    [Route("{id:guid}/template")]
    public async Task<ApiResponse<DiagnosisTemplate>> GetDiagnosisTemplate(Guid id)
    {
        var result = await bus.InvokeAsync<DiagnosisTemplate>(new GetDiagnosisTemplateQuery(id));
        return ApiResponse<DiagnosisTemplate>.Ok(result);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ApiResponse> UpdateDisease(Guid id,
        [FromBody] UpdateDiseaseCommand request)
    {
        request.Id = id;
        await bus.InvokeAsync(request);
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ApiResponse> DeleteDisease(Guid id)
    {
        await bus.InvokeAsync(new DeleteDiseaseCommand(id));
        return ApiResponse.Ok(statusCode: StatusCodes.Status204NoContent);
    }

    [HttpPost]
    [Route("{id:guid}/causes")]
    public async Task<ApiResponse<CreateDiseasePathogenResult>> AddDiseasePathogen(Guid id,
        [FromBody] CreateDiseasePathogenCommand request)
    {
        request.DiseaseId = id;
        var result = await bus.InvokeAsync<CreateDiseasePathogenResult>(request);
        return ApiResponse<CreateDiseasePathogenResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpPost]
    [Route("{id:guid}/icu-hospitalize-criteria")]
    public async Task<ApiResponse<CreateIcuHospitalizeCriterionResult>> AddIcuHospitalizeCriterion(Guid id,
        [FromBody] CreateIcuHospitalizeCriterionCommand request)
    {
        request.DiseaseId = id;
        var result = await bus.InvokeAsync<CreateIcuHospitalizeCriterionResult>(request);
        return ApiResponse<CreateIcuHospitalizeCriterionResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpPost]
    [Route("{id:guid}/resistance-risk-factors")]
    public async Task<ApiResponse<CreateResistanceRiskFactorResult>> AddResistanceRiskFactor(Guid id,
        [FromBody] CreateResistanceRiskFactorCommand request)
    {
        request.DiseaseId = id;
        var result = await bus.InvokeAsync<CreateResistanceRiskFactorResult>(request);
        return ApiResponse<CreateResistanceRiskFactorResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }

    [HttpPost]
    [Route("{id:guid}/treatment-protocols")]
    public async Task<ApiResponse<CreateTreatmentProtocolResult>> AddTreatmentProtocol(Guid id,
        [FromBody] CreateTreatmentProtocolCommand request)
    {
        request.DiseaseId = id;
        var result = await bus.InvokeAsync<CreateTreatmentProtocolResult>(request);
        return ApiResponse<CreateTreatmentProtocolResult>.Ok(result, statusCode: StatusCodes.Status201Created);
    }
}