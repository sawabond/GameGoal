using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public sealed class AppUser : IdentityUser<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Gender { get; set; } = "Male";

    public ICollection<AppRole> Roles { get; set; }
}