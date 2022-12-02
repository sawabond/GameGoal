using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class AchievementSystemRepository : DataRepository<AchievementSystem>, IAchievementSystemRepository
{
    public AchievementSystemRepository(ApplicationContext context)
        : base(context)
    {

    }

    private DbSet<AchievementSystem> AchievementSystems => _context.Set<AchievementSystem>();

    public async Task<AchievementSystem> GetIncludingAll(string id)
    {
        var achievementSystem = await AchievementSystems
            .Where(a => a.Id == id)
            .Include(a => a.Achievements)
            .Include(a => a.RelativeAchievements)
            .Include(a => a.MeasurableAchievements)
            .FirstOrDefaultAsync();

        return achievementSystem;
    }
}
