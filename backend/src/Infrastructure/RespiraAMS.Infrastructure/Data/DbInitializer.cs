using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Domain.Enums;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Infrastructure.Data;

public class DbInitializer
{
    private static async Task<bool> HasAnyData(AppDbContext context)
    {
        if (await context.Antibiotics.AnyAsync())
        {
            return true;
        }

        if (await context.AntibioticSpectra.AnyAsync())
        {
            return true;
        }

        if (await context.Criteria.AnyAsync())
        {
            return true;
        }

        if (await context.Diseases.AnyAsync())
        {
            return true;
        }

        if (await context.DiseasePathogens.AnyAsync())
        {
            return true;
        }

        if (await context.IcuHospitalizeCriteria.AnyAsync())
        {
            return true;
        }

        if (await context.Pathogens.AnyAsync())
        {
            return true;
        }

        if (await context.ResistanceRiskFactors.AnyAsync())
        {
            return true;
        }

        if (await context.TreatmentProtocols.AnyAsync())
        {
            return true;
        }

        return false;
    }

    public static async Task InitializeAsync(AppDbContext context, ILogger<DbInitializer> logger)
    {
        // Wait for migration to run
        await context.Database.MigrateAsync();

        // Check if there is any data in the database, if any, we won't seed data
        if (await HasAnyData(context))
        {
            logger.LogInformation("Database has data, skip seeding");
            return;
        }

        // Create sample data
        var disease = new Disease()
        {
            Name = "Viêm phổi cộng đồng",
            Description =
                "Viêm phổi cộng đồng là tình trạng nhiễm trùng của nhu mô phổi xảy ra ở cộng đồng, bên ngoài bệnh viện, bao gồm viêm phế nang, ống và túi phế nang, tiểu phế quản tận hoặc viêm tổ chức kẽ của phổi.",
            RequiredIcuMainCriteria = 1,
            RequiredIcuSecondaryCriteria = 3
        };
        var pathogens = new List<Pathogen>()
        {
            new() { Name = "Chlamydia pnuemoniae", Description = "bacterium" },
            new() { Name = "Haemophilus influenzae", Description = "bacterium" },
            new() { Name = "Legionella spp", Description = "bacterium" },
            new() { Name = "Mycoplasma pneumoniae", Description = "bacterium" },
            new() { Name = "Pseudomonas aeruginosae", Description = "bacterium" },
            new() { Name = "Staphylococus aureus", Description = "bacterium" },
            new() { Name = "Streptococcus pneumoniae", Description = "bacterium" },
            new() { Name = "Vi khuẩn gram âm đường ruột", Description = "bacterium" },
            new() { Name = "Virus hô hấp", Description = "virus" }
        };
        var spectra = new List<AntibioticSpectrum>()
        {
            new()
            {
                Name = "Aminoglycosid",
                Description =
                    "Thường được sử dụng để điều trị nhiễm trùng do vi khuẩn gram âm, đặc biệt là trong các trường hợp nhiễm trùng nặng"
            },
            new()
            {
                Name = "β-lactam",
                Description = "Hiệu quả đối với nhiều loại vi khuẩn gram dương và một số vi khuẩn gram âm"
            },
            new()
            {
                Name = "Macrolid",
                Description =
                    "Hiệu quả với vi khuẩn gram dương và một số vi khuẩn gram âm, thường được dùng để điều trị các bệnh đường hô hấp."
            },
            new()
            {
                Name = "Fluoroquinolon",
                Description =
                    "Đối với nhiều loại vi khuẩn gram âm và một số gram dương, thường được chỉ định trong các trường hợp nhiễm trùng đường tiết niệu và hô hấp"
            },
            new() { Name = "Others", Description = "Một vài phổ kháng sinh khác" }
        };
        var antibiotics = new List<Antibiotic>()
        {
            new()
            {
                Name = "Amoxicillin/ clavulanat",
                AntibioticSpectrumId = spectra.Where(x => x.Name.Equals("β-lactam")).Select(x => x.Id).First(),
                Category = AwareCategory.Access,
                RouteOfAdministrations = [RouteOfAdministration.Oral],
                Dosages = new Dictionary<RouteOfAdministration, List<string>>
                {
                    { RouteOfAdministration.Oral, ["500/125 mg mỗi 8h", "875/125 mg mỗi 12h", "2g/125 mg mỗi 12h"] },
                }
            },
            new()
            {
                Name = "Ampicillin/ sulbactam",
                AntibioticSpectrumId = spectra.Where(x => x.Name.Equals("β-lactam")).Select(x => x.Id).First(),
                Category = AwareCategory.Access,
                RouteOfAdministrations = [RouteOfAdministration.Intravenous],
                Dosages = new Dictionary<RouteOfAdministration, List<string>>
                {
                    { RouteOfAdministration.Intravenous, ["1.5 - 3 g mỗi 6h"] },
                }
            },
            new()
            {
                Name = "Azithromycin",
                AntibioticSpectrumId = spectra.Where(x => x.Name.Equals("Macrolid")).Select(x => x.Id).First(),
                Category = AwareCategory.Access,
                RouteOfAdministrations = [RouteOfAdministration.Intravenous, RouteOfAdministration.Oral],
                Dosages = new Dictionary<RouteOfAdministration, List<string>>
                {
                    { RouteOfAdministration.Intravenous, ["250-500 mg mỗi 24h"] },
                    { RouteOfAdministration.Oral, ["250-500 mg mỗi 24h"] },
                }
            },
            new()
            {
                Name = "Levofloxacin",
                AntibioticSpectrumId = spectra.Where(x => x.Name.Equals("Fluoroquinolon")).Select(x => x.Id).First(),
                Category = AwareCategory.Watch,
                RouteOfAdministrations = [RouteOfAdministration.Intravenous, RouteOfAdministration.Oral],
                Dosages = new Dictionary<RouteOfAdministration, List<string>>
                {
                    { RouteOfAdministration.Intravenous, ["750 mg mỗi 24h", "500 mg mỗi 12h"] },
                    { RouteOfAdministration.Oral, ["750 mg mỗi 24h", "500 mg mỗi 12h"] },
                }
            },
            new()
            {
                Name = "Moxifloxacin",
                AntibioticSpectrumId = spectra.Where(x => x.Name.Equals("Fluoroquinolon")).Select(x => x.Id).First(),
                Category = AwareCategory.Watch,
                RouteOfAdministrations = [RouteOfAdministration.Intravenous, RouteOfAdministration.Oral],
                Dosages = new Dictionary<RouteOfAdministration, List<string>>
                {
                    { RouteOfAdministration.Intravenous, ["400 mg mỗi 24h"] },
                    { RouteOfAdministration.Oral, ["400 mg mỗi 24h"] },
                }
            },
            new()
            {
                Name = "Vancomycin",
                AntibioticSpectrumId = spectra.Where(x => x.Name.Equals("Others")).Select(x => x.Id).First(),
                Category = AwareCategory.AccessWatch,
                RouteOfAdministrations = [RouteOfAdministration.Intravenous],
                Dosages = new Dictionary<RouteOfAdministration, List<string>>
                {
                    {
                        RouteOfAdministration.Intravenous,
                        [
                            "15-30 mg/kg mỗi 12h (không quá 2g/lần)",
                            "25-30mg/kg trong trường hợp nặng)"
                        ]
                    },
                }
            },
            new()
            {
                Name = "Ciprofloxacin",
                AntibioticSpectrumId = spectra.Where(x => x.Name.Equals("Fluoroquinolon")).Select(x => x.Id).First(),
                Category = AwareCategory.Access,
                RouteOfAdministrations = [RouteOfAdministration.Intravenous, RouteOfAdministration.Oral],
                Dosages = new Dictionary<RouteOfAdministration, List<string>>
                {
                    { RouteOfAdministration.Oral, ["500-750 mg/12h"] },
                    { RouteOfAdministration.Intravenous, ["400 mg mỗi 8-12h"] }
                }
            }
        };
        var icuHospitalizeCriteria = new List<IcuHospitalizeCriterion>()
        {
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new BooleanCriterion() { Name = "Cần thở máy xâm lấn" },
                IsMainCriteria = true
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new BooleanCriterion() { Name = "Sốc nhiễm khuẩn cần sử dụng thuốc vận mạch" },
                IsMainCriteria = true
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new NumericCriterion()
                {
                    Name = "Nhịp thở",
                    Min = 30,
                    Max = double.MaxValue,
                    Unit = "lần/phút",
                    IsExclusive = false
                },
                IsMainCriteria = false
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new NumericCriterion()
                {
                    Name = "Tỉ lệ PaO2/FiO2",
                    Min = 0,
                    Max = 250,
                    Unit = "",
                    IsExclusive = false,
                },
                IsMainCriteria = false,
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new BooleanCriterion() { Name = "Tổn thương nhiều thuỳ phổi" },
                IsMainCriteria = false
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new BooleanCriterion() { Name = "Giảm tri giác" },
                IsMainCriteria = false
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new NumericCriterion()
                {
                    Name = "Chỉ số tăng ure máu - BUN",
                    Min = 20,
                    Max = double.MaxValue,
                    Unit = "mg/dL",
                    IsExclusive = false,
                },
                IsMainCriteria = false
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new NumericCriterion()
                {
                    Name = "Giảm bạch cầu máu do nhiễm trùng",
                    Min = 0,
                    Max = 4000,
                    Unit = "mm3",
                    IsExclusive = true,
                },
                IsMainCriteria = false
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new NumericCriterion()
                {
                    Name = "Giảm tiểu cầu",
                    Min = 0,
                    Max = 100000,
                    Unit = "mm3",
                    IsExclusive = true,
                },
                IsMainCriteria = false
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new NumericCriterion()
                {
                    Name = "Giảm thân nhiệt",
                    Min = 0,
                    Max = 36,
                    Unit = "Celsius",
                    IsExclusive = true,
                },
                IsMainCriteria = false
            },
            new()
            {
                DiseaseId = disease.Id,
                Criterion = new BooleanCriterion() { Name = "Tụt huyết áp (cần truyền dịch tích cực)" }
            }
        };
        var resistanceRiskFactors = new List<ResistanceRiskFactor>()
        {
            new()
            {
                DiseaseId = disease.Id,
                Name = "S. pneumoniae kháng thuốc (DRSP – Drug Resistance Streptococcus pneumonia)",
                Criterion = new BooleanCriterion() { Name = "Có sử dụng Beta-lactam trong 3 tháng gần đây" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Streptococcus pneumoniae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "S. pneumoniae kháng thuốc (DRSP – Drug Resistance Streptococcus pneumonia)",
                Criterion = new BooleanCriterion() { Name = "Nghiện rượu" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Streptococcus pneumoniae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "S. pneumoniae kháng thuốc (DRSP – Drug Resistance Streptococcus pneumonia)",
                Criterion = new BooleanCriterion() { Name = "Người cao tuổi (> 65)" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Streptococcus pneumoniae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "S. pneumoniae kháng thuốc (DRSP – Drug Resistance Streptococcus pneumonia)",
                Criterion = new BooleanCriterion() { Name = "Suy giảm miễn dịch do bệnh lý hay dùng thuốc" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Streptococcus pneumoniae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "S. pneumoniae kháng thuốc (DRSP – Drug Resistance Streptococcus pneumonia)",
                Criterion = new BooleanCriterion() { Name = "Nhiều bệnh nội khoa kết hợp (bệnh tim, thận mạn)" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Streptococcus pneumoniae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "P. aeruginosa",
                Criterion = new BooleanCriterion() { Name = "Nhập viện gần đây" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Pseudomonas aeruginosae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "P. aeruginosa",
                Criterion = new BooleanCriterion() { Name = "Vừa mới điều trị kháng sinh" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Pseudomonas aeruginosae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "P. aeruginosa",
                Criterion = new BooleanCriterion() { Name = "Suy giảm miễn dịch" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Pseudomonas aeruginosae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "P. aeruginosa",
                Criterion = new BooleanCriterion()
                {
                    Name =
                        "Có bệnh phổi nền (xơ hoá nang, dãn phế quản, đợt cấp COPD nhập viện đòi hỏi sử dụng corticosteroid và kháng sinh thường xuyên)"
                },
                PathogenId = pathogens.Where(x => x.Name.Equals("Pseudomonas aeruginosae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "P. aeruginosa",
                Criterion = new BooleanCriterion() { Name = "Có nhiều bệnh đồng mắc (tiểu đường, nghiện rượu)" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Pseudomonas aeruginosae")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "Staphylococcus aureus kháng methicillin mắc phải cộng đồng (CA-MRSA)",
                Criterion = new BooleanCriterion()
                    { Name = "Viêm phổi nặng, bao gồm sốc nhiễm trùng và đòi hỏi thông khí cơ học" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Staphylococus aureus")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "Staphylococcus aureus kháng methicillin mắc phải cộng đồng (CA-MRSA)",
                Criterion = new BooleanCriterion()
                    { Name = "Viêm phổi hoại tử hoặc tạo hang và biến chứng mủ màng phổi" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Staphylococus aureus")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "Staphylococcus aureus kháng methicillin mắc phải cộng đồng (CA-MRSA)",
                Criterion = new BooleanCriterion() { Name = "Bệnh thận giai đoạn cuối" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Staphylococus aureus")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "Staphylococcus aureus kháng methicillin mắc phải cộng đồng (CA-MRSA)",
                Criterion = new BooleanCriterion() { Name = "Sử dụng thuốc đường tiêm" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Staphylococus aureus")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "Staphylococcus aureus kháng methicillin mắc phải cộng đồng (CA-MRSA)",
                Criterion = new BooleanCriterion() { Name = "Sống trong môi trường đông đúc" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Staphylococus aureus")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "Staphylococcus aureus kháng methicillin mắc phải cộng đồng (CA-MRSA)",
                Criterion = new BooleanCriterion() { Name = "Sau cảm cúm" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Staphylococus aureus")).Select(x => x.Id).First(),
            },
            new()
            {
                DiseaseId = disease.Id,
                Name = "Staphylococcus aureus kháng methicillin mắc phải cộng đồng (CA-MRSA)",
                Criterion = new BooleanCriterion() { Name = "Điều trị kháng sinh trong 3 tháng gần đây" },
                PathogenId = pathogens.Where(x => x.Name.Equals("Staphylococus aureus")).Select(x => x.Id).First(),
            },
        };
        var protocols = new List<TreatmentProtocol>()
        {
            new()
            {
                DiseaseId = disease.Id,
                Severity = Severity.Mild,
                TreatmentSite = TreatmentSite.Outpatient,
                SpecialInfectionId = pathogens.Where(x => x.Name.Equals("Streptococcus pneumoniae")).Select(x => x.Id)
                    .First(),
                OtherCriteria = [],
                Medicines =
                [
                    antibiotics.First(x => x.Name == "Levofloxacin"),
                    antibiotics.First(x => x.Name == "Moxifloxacin"),
                ],
            },
            new()
            {
                DiseaseId = disease.Id,
                Severity = Severity.Mild,
                TreatmentSite = TreatmentSite.Outpatient,
                SpecialInfectionId = null,
                OtherCriteria =
                [
                    resistanceRiskFactors.First(x => x.Criterion.Name == "Người cao tuổi (> 65)")
                        .Criterion,
                    resistanceRiskFactors.First(x => x.Criterion.Name == "Suy giảm miễn dịch")
                        .Criterion,
                    resistanceRiskFactors
                        .First(x => x.Criterion.Name == "Có nhiều bệnh đồng mắc (tiểu đường, nghiện rượu)")
                        .Criterion,
                ],
                Medicines =
                [
                    antibiotics.First(x => x.Name == "Levofloxacin"),
                    antibiotics.First(x => x.Name == "Moxifloxacin"),
                    antibiotics.First(x => x.Name == "Azithromycin"),
                ],
            },
            new()
            {
                DiseaseId = disease.Id,
                Severity = Severity.Moderate,
                TreatmentSite = TreatmentSite.Inpatient,
                SpecialInfectionId = null,
                OtherCriteria = [],
                Medicines =
                [
                    antibiotics.First(x => x.Name == "Levofloxacin"),
                    antibiotics.First(x => x.Name == "Moxifloxacin"),
                    antibiotics.First(x => x.Name == "Azithromycin"),
                ]
            },
            new()
            {
                DiseaseId = disease.Id,
                Severity = Severity.Severe,
                TreatmentSite = TreatmentSite.IntensiveCareUnit,
                SpecialInfectionId = null,
                OtherCriteria = [],
                Medicines =
                [
                    antibiotics.First(x => x.Name == "Levofloxacin"),
                    antibiotics.First(x => x.Name == "Moxifloxacin"),
                    antibiotics.First(x => x.Name == "Azithromycin"),
                ]
            },
            new()
            {
                DiseaseId = disease.Id,
                Severity = Severity.Severe,
                TreatmentSite = TreatmentSite.IntensiveCareUnit,
                SpecialInfectionId = pathogens.First(x => x.Name == "Pseudomonas aeruginosae")
                    .Id,
                OtherCriteria = [],
                Medicines =
                [
                    antibiotics.First(x => x.Name == "Levofloxacin"),
                    antibiotics.First(x => x.Name == "Moxifloxacin"),
                    antibiotics.First(x => x.Name == "Azithromycin"),
                    antibiotics.First(x => x.Name == "Ciprofloxacin"),
                ]
            },
            new()
            {
                DiseaseId = disease.Id,
                Severity = Severity.Severe,
                TreatmentSite = TreatmentSite.IntensiveCareUnit,
                SpecialInfectionId = pathogens.First(x => x.Name == "Staphylococus aureus").Id,
                OtherCriteria = [],
                Medicines =
                [
                    antibiotics.First(x => x.Name == "Vancomycin"),
                ]
            },
        };

        // Add sample data to database
        context.AntibioticSpectra.AddRange(spectra);
        context.Antibiotics.AddRange(antibiotics);
        context.Pathogens.AddRange(pathogens);
        context.Diseases.Add(disease);
        context.IcuHospitalizeCriteria.AddRange(icuHospitalizeCriteria);
        context.ResistanceRiskFactors.AddRange(resistanceRiskFactors);
        context.TreatmentProtocols.AddRange(protocols);

        var succeed = await context.SaveChangesAsync();
        if (succeed <= 0)
        {
            logger.LogError("Failed to seed data");
        }
        else
        {
            logger.LogInformation("Seed {count} data records in database", succeed);
        }
    }
}