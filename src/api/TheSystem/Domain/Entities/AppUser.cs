using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AppUser : IdentityUser<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Gender { get; set; }

    public virtual IEnumerable<AppRole> Roles { get; set; }

    public virtual ICollection<AchievementSystem>? AchievementSystems { get; set; }

    public virtual IEnumerable<Achievement> Achievements { get; set; }

    public virtual IEnumerable<RelativeAchievement> RelativeAchievements { get; set; }

    public virtual IEnumerable<MeasurableAchievement> MeasurableAchievements { get; set; }
}