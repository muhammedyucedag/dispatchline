using DispatchLine.Domain.Common;
using DispatchLine.Domain.Entities.Identity;

namespace DispatchLine.Domain.Entities;

public class Department : AuditableEntity, ISoftDeleteEntity
{
    public required string Name { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    //Releations
    public ICollection<User> Users { get; set; } = new HashSet<User>();

}