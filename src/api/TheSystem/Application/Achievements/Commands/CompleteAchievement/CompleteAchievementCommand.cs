using Application.Abstractions.Messaging;

namespace Application.Achievements.Commands.CompleteAchievement;

public sealed record CompleteAchievementCommand
    (string UserId,
    string Name): ICommand;
