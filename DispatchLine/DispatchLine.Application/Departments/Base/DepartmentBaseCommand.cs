using DispatchLine.Domain.Constants;
using FluentValidation;

namespace DispatchLine.Application.Departments.Base;

public record DepartmentBaseCommand
{
    /// <summary>
    /// Departman Adı
    /// </summary>
    /// <example>IT</example>
    public string Name { get; init; } = null!;
}

public class DepartmentBaseCommandValidator<T> : AbstractValidator<T> where T : DepartmentBaseCommand
{
    public DepartmentBaseCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(ConfigurationConsts.MaxTitleLength); 
    }
}