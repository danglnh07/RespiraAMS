using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Services.Contracts;

public interface IDiagnoseService
{
    Task<DiagnosisTemplateDto> GetDiagnosisTemplate(Guid diseaseId);
    Task<DiagnosisResultDto> Diagnose(Guid diseaseId, ClinicalPictureDto clinicalPicture);
    Task<IEnumerable<TreatmentProtocolDtoResponse>> Recommend(Guid diseaseId, RecommendDtoRequest req);
}