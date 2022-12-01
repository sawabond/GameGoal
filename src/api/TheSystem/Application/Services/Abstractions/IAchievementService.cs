using Domain.Entities;
using Domain.Shared;

namespace Application.Services.Abstractions;

public interface IAchievementService<TAchievement>
    where TAchievement : class
{
    Task<Result> CreateAchievement(AppUser user, TAchievement achievement);

    Task<Result> CreateAchievement(AchievementSystem user, TAchievement achievement);
}
