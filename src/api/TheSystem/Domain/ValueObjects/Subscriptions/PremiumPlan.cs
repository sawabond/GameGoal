namespace Domain.ValueObjects.Subscriptions;

public class PremiumPlan : BasicPlan
{
    public SubscriptionType Type { get; } = SubscriptionType.Premium;
}
