namespace Domain.Entities;

public class AppUserAchievement : Entity
{
    public string AppUserId { get; set; } = Guid.NewGuid().ToString();

    public string AchievementId { get; set; } = Guid.NewGuid().ToString();

    public Achievement Achievement { get; set; }

    public AppUser AppUser { get; set; }

}
