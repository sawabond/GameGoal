namespace Presentation.Requests;

public sealed record CreateAchievementRequest(
    string AchievementSystemId,
    string Name,
    string Description,
    bool IsNegative);