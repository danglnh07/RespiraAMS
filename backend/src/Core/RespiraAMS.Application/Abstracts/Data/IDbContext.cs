using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.Abstracts.Data;

public interface IDbContext
{
    DbSet<Pathogen> Pathogens { get; set; }
     DbSet<AntibioticSpectrum> AntibioticSpectra { get; set; }
     DbSet<Antibiotic> Antibiotics { get; set; }
     DbSet<Criterion> Criteria { get; set; }
     DbSet<IcuHospitalizeCriterion> IcuHospitalizeCriteria { get; set; }
     DbSet<ResistanceRiskFactor> ResistanceRiskFactors { get; set; }
     DbSet<Disease> Diseases { get; set; }
     DbSet<DiseasePathogen> DiseasePathogens { get; set; }
     DbSet<TreatmentProtocol> TreatmentProtocols { get; set; }

     Task<int> SaveChangesAsync();
     Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);
     Task ExecuteInTransactionAsync(Action action, CancellationToken cancellationToken = default);
}