using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserToken : IdentityUserToken<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
