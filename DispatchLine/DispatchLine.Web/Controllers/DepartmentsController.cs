using DispatchLine.Application.Departments.Command;
using DispatchLine.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable 1589

namespace DispatchLine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    /// <include file='XmlDocumentation/Departments.xml' path='docs/CreateDepartment/*'/>
    [HttpPost]
    public async Task<Created<Guid>> CreateDepartment([FromServices] ISender sender,
        [AsParameters] CreateDepartmentCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Department)}/{id}", id);
    }
}