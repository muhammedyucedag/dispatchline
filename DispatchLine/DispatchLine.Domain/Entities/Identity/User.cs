using DispatchLine.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace DispatchLine.Domain.Entities.Identity;

public class User : IdentityUser<Guid>, ISoftDeleteEntity
{
    public Guid DepartmentId { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PartyIdentification { get; set; }
    public required string Address { get; set; }
    public required string CityName { get; set; }
    public string? RefreshToken { get; set; }
    public string? EmergencyContactNumber { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
        
    //Relations
    public required Department Department { get; set; }
}