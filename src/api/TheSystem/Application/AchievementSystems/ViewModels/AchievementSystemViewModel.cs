using Application.Abstractions;
using Application.Achievements.ViewModels;

namespace Application.AchievementSystems.ViewModels;

public sealed record AchievementSystemViewModel
    (string Id,
    string AppUserId,
    string Name,
    string Description,
    ICollection<AchievementViewModel> Achievements) : IViewModel;
