namespace Domain.Entities;

public class Achievement : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string? Icon { get; set; }

    public bool IsAchieved { get; set; }

    public bool IsNegative { get; set; }

    public string? AchievementResult { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; }
}
