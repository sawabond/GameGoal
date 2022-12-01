using Application.Abstractions.Messaging;
using Domain.Shared;

namespace Application.AchievementSystems.Commands.CreateAchievementSystem;

public sealed record CreateAchievementSystemCommand
    (string CompanyId,
    string Name,
    string Description) : ICommand;
