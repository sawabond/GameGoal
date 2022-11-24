namespace Domain.Entities;

public class Achievement : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }

    public bool IsAchieved { get; set; }
}
