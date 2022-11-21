using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public sealed class AppUser : IdentityUser<string>
{
    [Required]
    public string Gender { get; set; } = "Male";

    public ICollection<AppUserRole> UserRoles { get; set; }
}