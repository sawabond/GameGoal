using Domain.Entities;
using Domain.Shared;

namespace Application.Services.Abstractions;

public interface IAchievementService<TAchievement>
    where TAchievement : class
{
    Task<Result> CreateForUser(string userId, TAchievement achievement);

    Task<Result> CreateAsPartOfSystem(string systemId, TAchievement achievement);
}
