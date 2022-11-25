using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserRole : IdentityUserRole<string>
{
    public override string UserId { get; set; } = Guid.NewGuid().ToString();

    public override string RoleId { get; set; } = Guid.NewGuid().ToString();

    public virtual AppUser User { get; set; }

    public virtual AppRole Role { get; set; }
}
