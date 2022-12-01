using Application.Abstractions;

namespace Application.Achievements.ViewModels;

public sealed record AchievementViewModel
    (string Id,
    string AchievementSystemId,
    string Name,
    string Description,
    bool IsNegative) : IViewModel;
