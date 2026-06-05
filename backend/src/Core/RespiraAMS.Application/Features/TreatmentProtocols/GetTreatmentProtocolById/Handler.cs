using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Application.Shared.Dtos;
using RespiraAMS.Domain.Enums;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Features.TreatmentProtocols.GetTreatmentProtocolById;

public class GetTreatmentProtocolByIdHandler(IDbContext context)
    : IQueryHandler<GetTreatmentProtocolByIdQuery, TreatmentProtocolResult>
{
    public async Task<TreatmentProtocolResult> HandleAsync(GetTreatmentProtocolByIdQuery query)
    {
        var protocol = await context.TreatmentProtocols
            .AsSplitQuery()
            .Where(x => x.Id == query.Id)
            .Select(x => new TreatmentProtocolResult()
            {
                Id = x.Id,
                LastUpdated = x.UpdatedAt,
                Name = x.Name,
                Issuer =  x.Issuer,
                IssueDate = x.IssueDate,
                Version =  x.Version,
                Severity =  x.Severity,
                TreatmentSite = x.TreatmentSite,
                SpecialInfection = x.SpecialInfection == null ? null : new PathogenItem()
                {
                    Id = x.SpecialInfection.Id,
                    Name = x.SpecialInfection.Name,
                    Description = x.SpecialInfection.Description,
                },
                OtherCriteria = x.OtherCriteria.Select(y => new CriterionItem()
                {
                    Id = y.Id,
                    Name = y.Name,
                    Type = y.Type,
                    Min = y.Type == CriterionType.Numeric ? ((NumericCriterion)y).Min : null,
                    Max = y.Type == CriterionType.Numeric ? ((NumericCriterion)y).Max : null,
                    Unit = y.Type == CriterionType.Numeric ? ((NumericCriterion)y).Unit : null,
                    IsExclusive = y.Type == CriterionType.Numeric ? ((NumericCriterion)y).IsExclusive : null
                }).ToList(),
                Medicines = x.Medicines.Select(y => new AntibioticItem()
                {
                    Id = y.Id,
                    Name = y.Name,
                    AntibioticSpectrum = new AntibioticSpectrumItem()
                    {
                        Id = y.AntibioticSpectrum.Id,
                        Name = y.AntibioticSpectrum.Name,
                        Description = y.AntibioticSpectrum.Description,
                    },
                    Category = y.Category,
                    RouteOfAdministrations = y.RouteOfAdministrations,
                    Dosages = y.Dosages,
                }).ToList(),
            })
            .FirstOrDefaultAsync();
        return protocol ?? throw new NotFoundException(nameof(TreatmentProtocol), query.Id);
    }
}