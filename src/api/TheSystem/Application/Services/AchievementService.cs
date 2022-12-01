using Application.Services.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.Services;

public sealed class AchievementService : IAchievementService<Achievement>
{
    private readonly IUnitOfWork _uow;

    public AchievementService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result> CreateAchievement(AchievementSystem system, Achievement achievement)
    {

    }

    public async Task<Result> CreateAchievement(AppUser user, Achievement achievement)
    {
        
    }
}
