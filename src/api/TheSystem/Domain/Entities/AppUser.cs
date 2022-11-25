using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AppUser : IdentityUser<string>
{
    public override string Id { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<AppUserRole> UserRoles { get; set; }

    public virtual ICollection<AchievementSystem>? AchievementSystems { get; set; }

    public virtual ICollection<Achievement> Achievements { get; set; }

    public virtual ICollection<RelativeAchievement> RelativeAchievements { get; set; }

    public virtual ICollection<MeasurableAchievement> MeasurableAchievements { get; set; }
}