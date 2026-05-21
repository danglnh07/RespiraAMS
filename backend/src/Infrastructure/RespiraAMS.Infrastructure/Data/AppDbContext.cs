using Microsoft.EntityFrameworkCore;
using RespiraAMS.Domain.Models;
using RespiraAMS.Infrastructure.Utils.Databases;

namespace RespiraAMS.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> context) : DbContext(context)
{
    public DbSet<Pathogen> Pathogens { get; set; }
    public DbSet<AntibioticSpectrum> AntibioticSpectra { get; set; }
    public DbSet<Antibiotic> Antibiotics { get; set; }
    public DbSet<Criterion> Criteria { get; set; }
    public DbSet<IcuHospitalizeCriterion> IcuHospitalizeCriteria { get; set; }
    public DbSet<ResistanceRiskFactor> ResistanceRiskFactors { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<DiseasePathogen> DiseasePathogens { get; set; }
    public DbSet<TreatmentProtocol> TreatmentProtocols { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Global query filter
        modelBuilder.Entity<Pathogen>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<AntibioticSpectrum>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Antibiotic>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Criterion>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<IcuHospitalizeCriterion>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<ResistanceRiskFactor>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<DiseasePathogen>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Disease>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<TreatmentProtocol>().HasQueryFilter(x => !x.IsDeleted);

        // Config each entity
        modelBuilder.Entity<Pathogen>()
            .ToTable("pathogens");
        modelBuilder.Entity<Pathogen>()
            .HasMany(x => x.DiseasePathogens)
            .WithOne(x => x.Pathogen)
            .HasForeignKey(x => x.PathogenId);
        modelBuilder.Entity<Pathogen>()
            .HasMany(x => x.ResistanceRiskFactors)
            .WithOne(x => x.Pathogen)
            .HasForeignKey(x => x.PathogenId);
        modelBuilder.Entity<Pathogen>()
            .HasMany(x => x.TreatmentProtocols)
            .WithOne(x => x.SpecialInfection)
            .HasForeignKey(x => x.SpecialInfectionId);

        modelBuilder.Entity<AntibioticSpectrum>()
            .ToTable("antibiotic_spectra");

        modelBuilder.Entity<Criterion>()
            .UseTphMappingStrategy()
            .Ignore(x => x.Type)
            .ToTable("criteria")
            .HasDiscriminator<string>("type")
            .HasValue<NumericCriterion>("numeric")
            .HasValue<BooleanCriterion>("boolean");

        modelBuilder.Entity<Antibiotic>()
            .ToTable("antibiotics");
        modelBuilder.Entity<Antibiotic>()
            .Property(x => x.Category)
            .HasConversion<string>();
        modelBuilder.Entity<Antibiotic>()
            .Property(x => x.RouteOfAdministrations)
            .HasConversion<string>();
        modelBuilder.Entity<Antibiotic>()
            .Property(x => x.Dosages)
            .HasConversion(DictionaryConverter.Converter)
            .Metadata.SetValueComparer(DictionaryConverter.DosageComparer);
        modelBuilder.Entity<Antibiotic>()
            .HasOne(x => x.AntibioticSpectrum)
            .WithMany(x => x.Antibiotics);

        modelBuilder.Entity<IcuHospitalizeCriterion>()
            .ToTable("icu_hospitalize_criteria");
        modelBuilder.Entity<IcuHospitalizeCriterion>()
            .HasOne(x => x.Criterion)
            .WithOne();
        modelBuilder.Entity<IcuHospitalizeCriterion>()
            .HasOne(x => x.Disease)
            .WithMany(x => x.IcuHospitalizeCriteria)
            .HasForeignKey(x => x.DiseaseId);

        modelBuilder.Entity<ResistanceRiskFactor>()
            .ToTable("resistance_risk_factors");
        modelBuilder.Entity<ResistanceRiskFactor>()
            .HasOne(x => x.Criterion)
            .WithOne();
        modelBuilder.Entity<ResistanceRiskFactor>()
            .HasOne(x => x.Disease)
            .WithMany(x => x.ResistanceRisks)
            .HasForeignKey(x => x.DiseaseId);

        modelBuilder.Entity<DiseasePathogen>()
            .ToTable("disease_pathogens");
        modelBuilder.Entity<DiseasePathogen>()
            .Property(x => x.Severity)
            .HasConversion<string>();
        modelBuilder.Entity<DiseasePathogen>()
            .Property(x => x.TreatmentSite)
            .HasConversion<string>();
        modelBuilder.Entity<DiseasePathogen>()
            .HasOne(x => x.Disease)
            .WithMany(x => x.DiseasePathogens)
            .HasForeignKey(x => x.DiseaseId);

        modelBuilder.Entity<Disease>()
            .ToTable("diseases");

        modelBuilder.Entity<TreatmentProtocol>()
            .ToTable("treatment_protocols");
        modelBuilder.Entity<TreatmentProtocol>()
            .Property(x => x.Severity)
            .HasConversion<string>();
        modelBuilder.Entity<TreatmentProtocol>()
            .Property(x => x.TreatmentSite)
            .HasConversion<string>();
        modelBuilder.Entity<TreatmentProtocol>()
            .Ignore(x => x.OtherCriteriaIds);
        modelBuilder.Entity<TreatmentProtocol>()
            .Ignore(x => x.MedicineIds);
        modelBuilder.Entity<TreatmentProtocol>()
            .HasMany(x => x.OtherCriteria)
            .WithMany();
        modelBuilder.Entity<TreatmentProtocol>()
            .HasMany(x => x.Medicines)
            .WithMany();
        modelBuilder.Entity<TreatmentProtocol>()
            .HasOne(x => x.Disease)
            .WithMany(x => x.TreatmentProtocols)
            .HasForeignKey(x => x.DiseaseId);
    }
}