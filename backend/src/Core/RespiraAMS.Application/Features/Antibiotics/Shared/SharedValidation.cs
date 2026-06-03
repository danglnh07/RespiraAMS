using FluentValidation;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.Shared;

public static class SharedValidation
{
    // Strictly, this is still validation, so it contextually correct to be placed in this class
    public static async Task<bool> IsAntibioticSpectrumIdExists(IDbContext context, Guid id)
    {
        return await context.AntibioticSpectra.FindAsync(id) is not null;
    }

    private static bool ValidateDosage(Dictionary<RouteOfAdministration, List<string>> dosages)
    {
        if (dosages.Count == 0)
        {
            return false;
        }

        foreach (var (key, value) in dosages)
        {
            // Validate key
            if (!Enum.IsDefined(key))
            {
                return false;
            }

            // Validate values
            if (value.Count == 0)
            {
                return false;
            }

            if (value.Any(string.IsNullOrWhiteSpace))
            {
                return false;
            }
        }

        return true;
    }

    public static IRuleBuilderOptions<T, Dictionary<RouteOfAdministration, List<string>>> IsDosagesValid<T>(
        this IRuleBuilder<T, Dictionary<RouteOfAdministration, List<string>>> ruleBuilder)
    {
        return ruleBuilder.Must(ValidateDosage).WithMessage("Antibiotic dosages is invalid");
    }
}