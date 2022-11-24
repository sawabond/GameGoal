namespace Domain.Entities;

public class AchievementSystem : Entity
{
    public string AppUserId { get; set; }
    public string Name { get; set; }

    public virtual IEnumerable<Achievement> Achievements { get; set; }

    public virtual IEnumerable<MeasurableAchievement> MeasurableAchievements { get; set; }

    public virtual IEnumerable<RelativeAchievement> RelativeAchievements { get; set; }
}
