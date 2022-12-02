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

    public async Task<Result> CreateAchievement(string systemId, Achievement achievement)
    {
        var createForSystemResult = await CreateAsPartOfSystem(systemId, achievement);

        if (!createForSystemResult.IsSuccess)
        {
            return createForSystemResult;
        }

        var createForUsersResult = await CreateForUsers(systemId, achievement);

        if (!createForUsersResult.IsSuccess)
        {
            return createForUsersResult;
        }

        return Result.Success();
    }

    private async Task<Result> CreateAsPartOfSystem(string systemId, Achievement achievement)
    {
        var system = await _uow.AchievementSystemRepository.GetIncludingAll(systemId);

        if (system is null)
        {
            return Result.Fail().WithError("Unable to create achievement - achievement system is not found");
        }

        system.Achievements.Add(achievement);

        return await _uow.ConfirmAsync()
            ? Result.Success()
            : Result.Fail().WithError($"Unable to create achievement - {achievement.Name} for system {system.Name}");
    }

    private async Task<Result> CreateForUsers(string systemId, Achievement achievement)
    {
        var system = await _uow.AchievementSystemRepository.GetAsync(systemId);

        if (system is null)
        {
            return Result.Fail().WithError("Unable to create achievement - achievement system is not found");
        }

        var user = await _uow.UserRepository.GetUserIncludingAll(system.AppUserId);

        if (user is null)
        {
            return Result.Fail().WithError("Unable to create achievement - user is not found");
        }

        user.CompanyMembers.ToList().ForEach(u =>
        {
            u.Achievements.Add(achievement);
        });

        return await _uow.ConfirmAsync()
            ? Result.Success()
            : Result.Fail()
            .WithError($"Unable to create achievement - {achievement.Name} for user {user.UserName} " +
            $"and achievement system {system.Name}");
    }
}
