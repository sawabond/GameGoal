using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.Repositories;

public sealed class AchievementSystemRepository : DataRepository<AchievementSystem>, IAchievementSystemRepository
{
    public AchievementSystemRepository(ApplicationContext context)
        : base(context)
    {

    }
}
