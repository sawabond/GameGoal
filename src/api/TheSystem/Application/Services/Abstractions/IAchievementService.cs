using Domain.Entities;
using Domain.Shared;

namespace Application.Services.Abstractions;

public interface IAchievementService<TAchievement>
    where TAchievement : Achievement
{
    Task<Result> CreateAchievement(string systemId, TAchievement achievement);
}
