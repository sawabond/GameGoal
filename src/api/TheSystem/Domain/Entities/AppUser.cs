using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AppUser : IdentityUser<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Gender { get; set; }

    public virtual ICollection<AppUserRole> UserRoles { get; set; }

    public virtual ICollection<AchievementSystem>? AchievementSystems { get; set; }

    public virtual ICollection<Achievement> Achievements { get; set; }

    public virtual ICollection<RelativeAchievement> RelativeAchievements { get; set; }

    public virtual ICollection<MeasurableAchievement> MeasurableAchievements { get; set; }

    [ForeignKey(nameof(Company))]
    public virtual string? CompanyId { get; set; }

    public virtual AppUser? Company { get; set; }

    public virtual ICollection<AppUser> CompanyMembers { get; set; }
}