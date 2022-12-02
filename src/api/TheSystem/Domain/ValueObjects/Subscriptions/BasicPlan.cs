using Domain.Entities;

namespace Domain.ValueObjects.Subscriptions;

public class BasicPlan
{
    public SubscriptionType Type { get; } = SubscriptionType.Basic;

    public AchievementSystem Smoker { get; } = new AchievementSystem
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Smoker",
        Description = "Achievement system aimed on decreasing time workers spend on smoking",
        Achievements = new[]
        {
            new Achievement
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Not to smoke",
                Description = "You should not smoke on the job at all",
                AchievementResult = "5% salary increasing",
                IsAchieved = false,
                IsNegative = false,
            }
        },
    };
}
