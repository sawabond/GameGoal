namespace Presentation.Requests;

public sealed record CreateAchievementSystemRequest(
    string Name,
    string Description);
