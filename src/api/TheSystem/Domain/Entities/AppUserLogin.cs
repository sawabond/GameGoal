using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserLogin : IdentityUserLogin<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
