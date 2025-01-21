using Ardalis.GuardClauses;
using AutoMapper;
using DispatchLine.Application.Common.GuardClauses;
using DispatchLine.Application.Departments.Base;
using DispatchLine.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatchLine.Application.Departments.Command;

public record CreateDepartmentCommand : DepartmentBaseCommand, IRequest<Guid>
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateDepartmentCommand, Domain.Entities.Department>();
        }
    }
}

public class CreateDepartmentValidator : DepartmentBaseCommandValidator<CreateDepartmentCommand>
{
    public CreateDepartmentValidator()
    {
    }
}

public class CreateDepartmentCommandHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateDepartmentCommand, Guid>
{
    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.AlreadyExist(await context.Departments.AnyAsync(x => x.Name == request.Name, cancellationToken), nameof(request.Name));

        var department = mapper.Map<Domain.Entities.Department>(request);
        
        await context.Departments.AddAsync(department, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return department.Id;
    }
}

