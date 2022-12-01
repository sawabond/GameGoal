using Domain.Entities;

namespace Domain.Abstractions;

public interface IAchievementSystemRepository : IDataRepository<AchievementSystem>
{
    Task<AchievementSystem> GetIncludingAll(string id);
}
