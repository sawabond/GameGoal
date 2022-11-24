using Application.Abstractions;

namespace Application.AchievementSystems.ViewModels;

public sealed record AchievementSystemViewModel
    (string AppUserId,
    string Name,
    string Description) : IViewModel;
