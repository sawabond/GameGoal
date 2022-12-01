using Application.Abstractions.Messaging;

namespace Application.Achievements.Commands.CreateAchievement;

public sealed record CreateAchievementCommand(
    string AchievementSystemId,
    string Name,
    string Description,
    bool IsNegative) : ICommand;
