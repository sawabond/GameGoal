using Application.Abstractions.Messaging;
using Domain.Shared;

namespace Application.AchievementSystems.Commands.CreateAchievementSystem;

public sealed record CreateAchievementSystemCommand
    (string AppUserId,
    string Name,
    string Description) : ICommand;
