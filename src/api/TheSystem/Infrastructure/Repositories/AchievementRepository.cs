using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.Repositories;

public sealed class AchievementRepository : DataRepository<Achievement>, IAchievementRepository
{
    public AchievementRepository(ApplicationContext context) : base(context)
    {
    }
}
