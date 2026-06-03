using BuildingBlocks.Dtos;
using FluentValidation;

namespace RespiraAMS.Application.Shared;

public static class SharedValidation
{
    public static IRuleBuilderOptions<T, PaginationParam> IsValidPaginationParam<T>(
        this IRuleBuilder<T, PaginationParam> ruleBuilder)
    {
        return ruleBuilder
            .Must(p => p.Page > 0)
            .WithMessage("Pagination page must be a positive integer")
            .Must(p => p.Size > 0)
            .WithMessage("Pagination size must be a positive integer");
    }

    public static IRuleBuilder<T, string> NotEmptyString<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
    {
        return ruleBuilder
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage($"{fieldName} cannot be empty");
    }

    public static IRuleBuilderOptions<T, int> IsPositive<T>(this IRuleBuilder<T, int> ruleBuilder, string fieldName)
    {
        return ruleBuilder.GreaterThan(0).WithMessage($"{fieldName} must be a positive integer");
    }

    public static IRuleBuilderOptions<T, Guid> IsValidGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder, string fieldName)
    {
        return ruleBuilder.NotEqual(Guid.Empty).WithMessage($"{fieldName} cannot be empty");
    }

    public static IRuleBuilderOptions<T, IList<TElement>> IsListNotEmpty<T, TElement>(
        this IRuleBuilder<T, IList<TElement>> ruleBuilder, string fieldName)
    {
        return ruleBuilder.Must(list => list.Count > 0).WithMessage($"{fieldName} cannot be empty");
    }

    public static IRuleBuilderOptions<T, TEnum> IsEnum<T, TEnum>(this IRuleBuilder<T, TEnum> ruleBuilder,
        string fieldName) where TEnum : struct, Enum
    {
        return ruleBuilder.IsInEnum().WithMessage($"{fieldName} is invalid");
    }
    
    public static IRuleBuilderOptions<T, IList<TEnum>> IsEnumList<T, TEnum>(
        this IRuleBuilder<T, IList<TEnum>> ruleBuilder,
        string fieldName)
        where TEnum : struct, Enum
    {
        return ruleBuilder
            .Must(list =>
                list.Count > 0 &&
                list.All(x => Enum.IsDefined(typeof(TEnum), x)))
            .WithMessage($"{fieldName} is invalid");
    }
}