using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Dtos;
using RespiraAMS.Application.Profiles.Implementations;
using RespiraAMS.Application.RepositoryContracts;
using RespiraAMS.Application.Services.Contracts;
using RespiraAMS.Domain.Exceptions;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Services.Implementations;

public class AntibioticSpectrumService(
    IUnitOfWork uow,
    AntibioticSpectrumProfile profile,
    ILogger<AntibioticSpectrumService> logger) : IAntibioticSpectrumService
{
    public async Task<Guid> CreateAsync(AntibioticSpectrumDtoRequest req)
    {
        // Map from request to model
        var spectrum = profile.ToModel(req);

        // Save to database
        await uow.Repo<AntibioticSpectrum>().CreateAsync(spectrum);
        
        // Return ID
        if (await uow.SaveChangesAsync() > 0) return spectrum.Id;
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to create antibiotic spectrum");
        throw new InternalServerErrorException();
    }

    public async Task<AntibioticSpectrumDtoResponse> GetByIdAsync(Guid id)
    {
        var spectrum = await uow.Repo<AntibioticSpectrum>().GetByIdAsync(id);
        return spectrum is null ? throw new NotFoundException(nameof(AntibioticSpectrum), id) : profile.ToResp(spectrum);
    }

    public async Task<(PaginationMetadata, IEnumerable<AntibioticSpectrumDtoResponse>)> GetListAsync(int page, int size)
    {
        var spectra = await uow.Repo<AntibioticSpectrum>().GetPagedListAsync(page, size);
        return profile.ToPagedResponse(spectra);
    }

    public async Task<AntibioticSpectrumDtoResponse> UpdateAsync(Guid id, AntibioticSpectrumDtoRequest req)
    {
        // Get antibiotic spectrum by ID
        var spectrum = await uow.Repo<AntibioticSpectrum>().GetByIdAsync(id);
        if (spectrum is null)
        {
            throw new NotFoundException(nameof(AntibioticSpectrum), id);
        }

        // Map from request to model
        spectrum = profile.MapModel(req, spectrum);
        
        // Update to database
        uow.Repo<AntibioticSpectrum>().Update(spectrum);
        
        // Since spectrum only has scalar fields, we just need to map them instead of refetch from database
        if (await uow.SaveChangesAsync() > 0) return profile.ToResp(spectrum);
        
        // Throw error if failed to save changes
        logger.LogWarning("Failed to update antibiotic spectrum");
        throw new InternalServerErrorException();
    }

    public async Task DeleteAsync(Guid id)
    {
        // Get spectrum by ID
        var spectrum = await uow.Repo<AntibioticSpectrum>()
            .GetByIdAsync(id, query => query.Include(x => x.Antibiotics));
        if (spectrum is null)
        {
            throw new NotFoundException(nameof(AntibioticSpectrum), id);
        }

        // Start delete with cascade transaction
        await uow.ExecuteInTransactionAsync(() =>
        {
            // Delete spectrum
            uow.Repo<AntibioticSpectrum>().SoftDelete(spectrum);
            
            // Cascade delete antibiotic
            logger.LogInformation("Start cascade delete antibiotic: {count}", spectrum.Antibiotics.Count);
            foreach (var antibiotic in spectrum.Antibiotics)
            {
                uow.Repo<Antibiotic>().SoftDelete(antibiotic);
            }
        });
    }
}