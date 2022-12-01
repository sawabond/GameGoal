namespace Domain.Entities;

public class AchievementSystem : Entity
{
    public string AppUserId { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Achievement> Achievements { get; set; }

    public virtual ICollection<MeasurableAchievement> MeasurableAchievements { get; set; }

    public virtual ICollection<RelativeAchievement> RelativeAchievements { get; set; }
}
