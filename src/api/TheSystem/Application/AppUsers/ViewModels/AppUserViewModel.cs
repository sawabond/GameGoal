using Application.Abstractions;
using Application.Achievements.ViewModels;
using Application.AchievementSystems.ViewModels;

namespace Application.AppUsers.ViewModels;

public sealed class AppUserViewModel : IViewModel
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Token { get; set; }

    public IEnumerable<AchievementSystemViewModel> AchievementSystems { get; set; }

    public IEnumerable<AchievementViewModel> Achievements { get; set; }
}
